using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Helpers;
using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.DTO.Output.Review;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.Assemblers;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [CustomAuthorize(UserRolesConstant.ADMIN)]
    [ApiController]
    public class ReviewsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<ReviewOutputDTO>>> GetReviews([FromQuery] ReviewFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var total = await _unitOfWork.ReviewCEN.GetTotal(filters);

                var itemResults = (await _unitOfWork.ReviewCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.Admin)
                                        .ThenInclude(x=>x.ApplicationUser)
                                        .Include(x => x.Boat)
                                        .ThenInclude(x=>x.BoatInfo)
                     ))
                    .Select(x => ReviewAssemblers.Convert(x));

                return new GenericResponseDTO<ReviewOutputDTO>(itemResults, pagination.Limit, pagination.Offset, total);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewOutputDTO>> GetReviewEN(int id)
        {
            try
            {
                var itemResult = await _unitOfWork.ReviewCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new ReviewFilters
                {
                    Id = id
                }, includeProperties: source => source.Include(x => x.Admin)
                                        .ThenInclude(x => x.ApplicationUser)
                                        .Include(x => x.Boat)
                                        .ThenInclude(x => x.BoatInfo));

                var review = itemResult.Select(x => ReviewAssemblers.Convert(x)).FirstOrDefault();
                if (review == null)
                {
                    return NotFound();
                }

                return review;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/close")]
        public async Task<IActionResult> PutCloseReviewEN(int id)
        {
            try
            {
                await _unitOfWork.ReviewCEN.CloseReview(id);

                return NoContent();
            }
            catch (DataValidationException dataValidation)
            {
                if (dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }


    }
}
