using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.Helpers;
using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.ClientInvoice;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientInvoiceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public ClientInvoiceController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        // GET: api/ClientInvoice
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<ClientInvoiceOutputDTO>>> GetClientInvoices([FromQuery] ClientInvoiceFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var clientInvoiceTotal = await _unitOfWork.ClientInvoiceCEN.GetTotal(filters);

                var clientInvoices = (await _unitOfWork.ClientInvoiceCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x=>x.InvoiceLines)
                                                        .Include(x => x.Client)
                                                        .ThenInclude(x=>x.ApplicationUser)
                    ))
                    .Select(x => ClientInvoiceAssemblers.Convert(x));

                return new GenericResponseDTO<ClientInvoiceOutputDTO>(clientInvoices, pagination.Limit, pagination.Offset, clientInvoiceTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/ClientInvoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientInvoiceOutputDTO>> GetClientInvoice(int id)
        {
            try
            {
                var clientInvoices = await _unitOfWork.ClientInvoiceCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new ClientInvoiceFilters
                {
                    Id = id
                }, includeProperties: source => source.Include(x => x.InvoiceLines)
                                        .Include(x => x.Client)
                                        .ThenInclude(x => x.ApplicationUser));

                var clientInvoice = clientInvoices.Select(x => ClientInvoiceAssemblers.Convert(x)).FirstOrDefault();
                if (clientInvoice == null)
                {
                    return NotFound();
                }

                return clientInvoice;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }
        
        // PUT: api/ClientInvoice/5/cancel
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> PutCancelClientInvoice(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.ClientInvoiceCEN.CancelClientInvoice(id);

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
