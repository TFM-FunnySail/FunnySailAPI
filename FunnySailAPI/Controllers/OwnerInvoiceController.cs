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
                                        .Include(x => x.Owner));

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

        // POST: api/OwnerInvoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<OwnerInvoiceEN>> PostOwnerInvoice(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                ApplicationUser user;
                if (!RolesHelpers.AnyRole(UserRoles, UserRolesConstant.ADMIN))
                {
                    user = User.ApplicationUser;
                }
                else
                {
                    user = await _unitOfWork.UserManager.FindByIdAsync(addOwnerInvoiceInput.OwnerId);
                }
                addOwnerInvoiceInput.OwnerId = user.Id;

                int ownerInvoiceId = await _unitOfWork.OwnerInvoiceCP.CreateOwnerInvoice(addOwnerInvoiceInput);

                return CreatedAtAction("GetOwnerInvoice", new { id = addOwnerInvoiceInput.OwnerId });
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


    }
}
