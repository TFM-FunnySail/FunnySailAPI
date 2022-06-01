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
using FunnySailAPI.DTO.Output.Service;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Sercices;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public ServicesController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<ServiceOutputDTO>>> GetServices([FromQuery] ServiceFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                int serviceTotal = await _unitOfWork.ServiceCEN.GetTotal(filters);

                var services = (await _unitOfWork.ServiceCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination()               
                     ))
                    .Select(x => ServiceAssemblers.Convert(x));

                return new GenericResponseDTO<ServiceOutputDTO>(services, pagination.Limit, pagination.Offset, serviceTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOutputDTO>> GetService(int id)
        {
            try
            {
                var services = await _unitOfWork.ServiceCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new ServiceFilters
                {
                    ServiceId = id
                });

                var service = services.Select(x => ServiceAssemblers.Convert(x)).FirstOrDefault();
                if (service == null)
                {
                    return NotFound();
                }

                return service;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceEN(int id, UpdateServiceDTO updateServiceInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateServiceInput.Id)
                    return BadRequest();

                await _unitOfWork.ServiceCEN.UpdateService(updateServiceInput);

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

        // POST: api/Services
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<ServiceOutputDTO>> PostServices(AddServiceDTO serviceInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int serviceId = await _unitOfWork.ServiceCEN.CreateService(serviceInput);

                return CreatedAtAction("GetService", new { id = serviceId });
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

        // DEL: api/Services/5/delete
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.ServiceCEN.DeleteService(id);

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
