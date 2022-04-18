using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.Helpers;
using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.DTO.Output.Activity;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice;
using FunnySailAPI.DTO.Output.OwnerInvoice;
using FunnySailAPI.DTO.Output.User;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerInvoiceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public OwnerInvoiceController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        // GET: api/OwnerInvoice
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<OwnerInvoiceOutputDTO>>> GetOwnerInvoices([FromQuery] OwnerInvoiceFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var ownerInvoiceTotal = await _unitOfWork.OwnerInvoiceCEN.GetTotal(filters);

                var ownerInvoices = (await _unitOfWork.OwnerInvoiceCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.OwnerInvoiceLines)
                                        .ThenInclude(x => x.Booking)
                                        .Include(x => x.TechnicalServiceBoats)
                                        .Include(x => x.Owner)
                                        .ThenInclude(x => x.ApplicationUser)

                     ))
                    .Select(x => OwnerInvoiceAssemblers.Convert(x));

                return new GenericResponseDTO<OwnerInvoiceOutputDTO>(ownerInvoices, pagination.Limit, pagination.Offset, ownerInvoiceTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/OwnerInvoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerInvoiceOutputDTO>> GetOwnerInvoice(int id)
        {
            try
            {
                var ownerInvoices = await _unitOfWork.OwnerInvoiceCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new OwnerInvoiceFilters
                {
                    Id = id
                }, includeProperties: source => source.Include(x => x.OwnerInvoiceLines)
                                        .ThenInclude(x => x.Booking)
                                        .Include(x => x.TechnicalServiceBoats)
                                        .Include(x => x.Owner)
                                        .ThenInclude(x=>x.ApplicationUser)
                                        );

                var ownerInvoice = ownerInvoices.Select(x => OwnerInvoiceAssemblers.Convert(x)).FirstOrDefault();
                if (ownerInvoice == null)
                {
                    return NotFound();
                }

                return ownerInvoice;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }
        

        // PUT: api/OwnerInvoice/5/cancel
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> PutCancelOwnerInvoice(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.OwnerInvoiceCEN.CancelOwnerInvoice(id);

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

        // PUT: api/OwnerInvoice/5/pay
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> PutPayOwnerInvoice(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.OwnerInvoiceCEN.PayOwnerInvoice(id);

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

        // GET: api/OwnerInvoice/invoiceOrderPending
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpGet("invoiceOrderPending")]
        public async Task<ActionResult<GenericResponseDTO<OwnerInvoiceLinesOutputDTO>>> GetOwnerInvoicesOrderPending([FromQuery] OwnerInvoiceLineFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                filters.Invoiced = false;

                int ownerInvoiceTotal = await _unitOfWork.OwnerInvoiceLineCEN.GetTotal(filters);

                var ownerInvoices = (await _unitOfWork.OwnerInvoiceLineCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.Owner)
                                        .ThenInclude(x => x.ApplicationUser)

                     ))
                    .Select(x => OwnerInvoiceLineAssemblers.Convert(x));
                

                return new GenericResponseDTO<OwnerInvoiceLinesOutputDTO>(ownerInvoices, pagination.Limit, pagination.Offset, ownerInvoiceTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/OwnerInvoice/invoiceOrderPending/owner
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpGet("invoiceOrderPending/owner")]
        public async Task<ActionResult<IEnumerable<UserOutputDTO>>> GetOwnerWithInvoicesOrderPending()
        {
            try
            {
                var owners = (await _unitOfWork.UserCEN.GetOwnerWithInvPending())
                    .Select(x => UserAssemblers.Convert(x));

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPost]
        public async Task<ActionResult<OwnerInvoiceOutputDTO>> PostOwnerInvoice(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int id = await _unitOfWork.OwnerInvoiceCP.CreateOwnerInvoice(addOwnerInvoiceInput);

                return CreatedAtAction("GetOwnerInvoice", new { id = id });
            }
            catch (DataValidationException dataValidation)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }

        }

    }
}
