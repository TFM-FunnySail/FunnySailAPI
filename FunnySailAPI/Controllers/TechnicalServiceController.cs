using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.DTO.Output.TechnicalService;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalServiceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public TechnicalServiceController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<TechnicalServiceOutputDTO>>> GetTechnicalService([FromQuery] TechnicalServiceFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var technicalServiceTotal = await _unitOfWork.TechnicalServiceCEN.GetTotal(filters);

                var technicalServices = (await _unitOfWork.TechnicalServiceCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination()
                    ))
                    .Select(x => TechnicalServiceAssembler.Convert(x));

                return new GenericResponseDTO<TechnicalServiceOutputDTO>(technicalServices, pagination.Limit, pagination.Offset, technicalServiceTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        //GET: api/TechnicalService/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnicalServiceOutputDTO>> GetTechnicalService(int id)
        {
            try
            {
                var TechnicalServices = await _unitOfWork.TechnicalServiceCEN.GetAll(
                    pagination: new Pagination
                    {
                        Limit = 1,
                        Offset = 0
                    }, filters: new TechnicalServiceFilters
                    {
                        Id = id
                    });

                var TechnicalService = TechnicalServices.Select(x => TechnicalServiceAssembler.Convert(x)).FirstOrDefault();
                if (TechnicalService == null)
                {
                    return NotFound();
                }

                return TechnicalService;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }


        [HttpPost]
        public async Task<ActionResult<TechnicalServiceOutputDTO>> CreateTechnicalServiceController(decimal price, string description)
        {
            try
            {
                int id = await _unitOfWork.TechnicalServiceCEN.AddTechnicalService(price, description);

                return CreatedAtAction("GetTechnicalService", new { id = id });
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

        //POST: api/TechnicalService/Schedule
        [HttpPost("schedule")]
        public async Task<ActionResult<TechnicalServiceOutputDTO>> ScheduleTechnicalServiceBoatController(ScheduleTechnicalServiceDTO scheduleTechnicalService)
        {
            try
            {
                int id = await _unitOfWork.TechnicalServiceCP.ScheduleTechnicalServiceToBoat(scheduleTechnicalService);
                
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

        // PUT: api/TechnicalService/5/cancel
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelTechnicalService(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.TechnicalServiceCEN.CancelTechnicalService(id);

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

        // PUT: api/TechnicalService/5
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnicalService(int id, UpdateTechnicalServiceDTO technicalService)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                technicalService.Id = id;

                await _unitOfWork.TechnicalServiceCEN.UpdateTechnicalService(technicalService);

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

        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnicalService(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.TechnicalServiceCEN.DeleteService(id);

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
