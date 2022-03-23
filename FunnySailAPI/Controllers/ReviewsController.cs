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
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ReviewEN>> GetReviewEN(int id)
        //{
        //    var reviewEN = await _context.Reviews.FindAsync(id);

        //    if (reviewEN == null)
        //    {
        //        return NotFound();
        //    }

        //    return reviewEN;
        //}

        //// PUT: api/Reviews/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReviewEN(int id, ReviewEN reviewEN)
        //{
        //    if (id != reviewEN.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(reviewEN).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReviewENExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Reviews
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<ReviewEN>> PostReviewEN(ReviewEN reviewEN)
        //{
        //    _context.Reviews.Add(reviewEN);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetReviewEN", new { id = reviewEN.Id }, reviewEN);
        //}

        //// DELETE: api/Reviews/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ReviewEN>> DeleteReviewEN(int id)
        //{
        //    var reviewEN = await _context.Reviews.FindAsync(id);
        //    if (reviewEN == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Reviews.Remove(reviewEN);
        //    await _context.SaveChangesAsync();

        //    return reviewEN;
        //}

    }
}
