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
using FunnySailAPI.DTO.Output.Activity;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Activity;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public ActivitiesController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        // GET: api/Actvities
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<ActivityOutputDTO>>> GetActivities([FromQuery] ActivityFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                int activityTotal = await _unitOfWork.ActivityCEN.GetTotal(filters);

                var activities = (await _unitOfWork.ActivityCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.ActivityResources)
                                        .ThenInclude(x => x.Resource)
                                     
                     ))
                    .Select(x => ActivityAssemblers.Convert(x));

                return new GenericResponseDTO<ActivityOutputDTO>(activities, pagination.Limit, pagination.Offset, activityTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityOutputDTO>> GetActivity(int id)
        {
            try
            {
                var activities = await _unitOfWork.ActivityCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new ActivityFilters
                {
                    ActivityId = id
                }, includeProperties: source => source.Include(x => x.ActivityResources)
                                        .ThenInclude(x => x.Resource));

                var activity = activities.Select(x => ActivityAssemblers.Convert(x)).FirstOrDefault();
                if (activity == null)
                {
                    return NotFound();
                }

                return activity;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }


        // GET: api/Activities/availableActivities
        [HttpGet("availableActivities")]
        public async Task<ActionResult<GenericResponseDTO<ActivityOutputDTO>>> GetAvailableActivities(DateTime initialDate, DateTime endDate, [FromQuery] Pagination pagination)
        {
            try
            {
                var activityTotal = await _unitOfWork.ActivityCEN.GetTotal();

                var activities = (await _unitOfWork.ActivityCEN.GetAvailableActivities(pagination: pagination ?? new Pagination(),
                    initialDate: initialDate,
                    endDate: endDate,
                    includeProperties: source => source.Include(x => x.ActivityResources)
                                        .ThenInclude(x => x.Resource)
                     ))
                    .Select(x => ActivityAssemblers.Convert(x));

                return new GenericResponseDTO<ActivityOutputDTO>(activities, pagination.Limit, pagination.Offset, activityTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityEN(int id, UpdateAcitivityInputDTO updateActivityInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateActivityInput.Id)
                    return BadRequest();

                await _unitOfWork.ActivityCEN.EditActivity(updateActivityInput);

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

        // POST: api/Activities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<ActivityOutputDTO>> PostActivities(AddActivityInputDTO activityInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int activityId = await _unitOfWork.ActivityCEN.AddActivity(activityInput);

                return CreatedAtAction("GetActivity", new { id = activityId });
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

        // PUT: api/Activities/5/activate 
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> PutActiveActivity(int id)
        {
            try
            {
                await _unitOfWork.ActivityCEN.ActivateActivity(id);

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

        // PUT: api/Activities/5/deactivate 
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> PutDisapproveBoat(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.ActivityCEN.DeactivateActivity(id);

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
