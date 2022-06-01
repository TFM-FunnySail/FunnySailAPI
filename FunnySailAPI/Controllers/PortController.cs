using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Port;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.DTO.Output.Port;
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
    public class PortController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public PortController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Ports
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<PortOutputDTO>>> GetPorts([FromQuery] PortFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var total = await _unitOfWork.PortCEN.GetTotal(filters);

                var itemResults = (await _unitOfWork.PortCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.Moorings)
                                                        .ThenInclude(x=>x.Boat)))
                    .Select(x => PortAssemblers.Convert(x));

                return new GenericResponseDTO<PortOutputDTO>(itemResults, pagination.Limit, pagination.Offset, total);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Ports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortOutputDTO>> GetPort(int id)
        {
            try
            {
                var itemResult = await _unitOfWork.PortCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new PortFilters
                {
                    Id = id
                }, includeProperties: source => source.Include(x => x.Moorings)
                                                        .ThenInclude(x => x.Boat));

                var port = itemResult.Select(x => PortAssemblers.Convert(x)).FirstOrDefault();
                if (port == null)
                {
                    return NotFound();
                }

                return port;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }


        // Post: api/Ports
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<PortOutputDTO>> CreatePort(AddPortInputDTO portInputDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int portId = await _unitOfWork.PortCEN.AddPort(portInputDTO);
                return CreatedAtAction("GetBoat", new { id = portId });
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

        // Put: api/Ports/5
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<ActionResult<PortOutputDTO>> EditPort(UpdatePortDTO updatePortDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.PortCEN.EditPort(updatePortDTO);

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

        // Put: api/Ports/5
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PortOutputDTO>> DeletePort(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.PortCEN.DeletePort(id);

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
