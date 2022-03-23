using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.DTO.Output.Refund;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [CustomAuthorize(UserRolesConstant.ADMIN)]
    [ApiController]
    public class RefundsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefundsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/refunds
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<RefundOutputDTO>>> GetBoats([FromQuery] RefundFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var boatTotal = await _unitOfWork.RefundCEN.GetTotal(filters);

                var boats = (await _unitOfWork.RefundCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.ClientInvoice)
                     ))
                    .Select(x => RefundAssemblers.Convert(x));

                return new GenericResponseDTO<RefundOutputDTO>(boats, pagination.Limit, pagination.Offset, boatTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }
    }
}
