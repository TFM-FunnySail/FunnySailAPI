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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.Helpers;
using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Boats
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<BoatOutputDTO>>> GetBoats([FromQuery] BoatFilters filters,[FromQuery] Pagination pagination)
        {
            try
            {
                var boatTotal = await _unitOfWork.BoatCEN.GetTotal(filters);

                var boats = (await _unitOfWork.BoatCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source=>source.Include(x=>x.BoatInfo)
                                        .Include(x => x.BoatPrices)
                                        .Include(x=>x.RequiredBoatTitles)
                                        .Include(x => x.BoatType)
                                        .Include(x => x.Mooring)
                                        .ThenInclude(x => x.Port)
                                        .Include(x => x.BoatResources)
                                        .ThenInclude(x=>x.Resource)
                     ))
                    .Select(x=> BoatAssemblers.Convert(x));

                return new GenericResponseDTO<BoatOutputDTO>(boats,pagination.Limit,pagination.Offset,boatTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Boats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoatOutputDTO>> GetBoat(int id)
        {
            try
            {
                var boats = await _unitOfWork.BoatCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new BoatFilters
                {
                    BoatId = id
                }, includeProperties: source => source.Include(x => x.BoatInfo)
                                         .Include(x => x.BoatPrices)
                                         .Include(x => x.RequiredBoatTitles)
                                         .Include(x => x.BoatType)
                                         .Include(x => x.Mooring)
                                         .ThenInclude(x => x.Port)
                                         .Include(x => x.BoatResources)
                                         .ThenInclude(x => x.Resource));

                var boat = boats.Select(x => BoatAssemblers.Convert(x)).FirstOrDefault();
                if (boat == null)
                {
                    return NotFound();
                }

                return boat;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Boats/availableBoats
        [HttpGet("availableBoats")]
        public async Task<ActionResult<GenericResponseDTO<BoatOutputDTO>>> GetAvailableBoats(DateTime initialDate,DateTime endDate,[FromQuery]Pagination pagination)
        {
            try
            {
                var boatTotal = await _unitOfWork.BoatCEN.GetTotal();

                var boats = (await _unitOfWork.BoatCEN.GetAvailableBoats(pagination: pagination ?? new Pagination(),
                    initialDate: initialDate, 
                    endDate: endDate,
                    includeProperties: source => source.Include(x => x.BoatInfo)
                                        .Include(x => x.BoatPrices)
                                        .Include(x => x.RequiredBoatTitles)
                                        .Include(x => x.BoatType)
                                        .Include(x => x.Mooring)
                                        .ThenInclude(x => x.Port)
                                        .Include(x => x.BoatResources)
                                        .ThenInclude(x => x.Resource)
                     ))
                    .Select(x => BoatAssemblers.Convert(x));

                return new GenericResponseDTO<BoatOutputDTO>(boats, pagination.Limit, pagination.Offset, boatTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Boats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize(UserRolesConstant.ADMIN, UserRolesConstant.BOAT_OWNER)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoatEN(int id, UpdateBoatInputDTO updateBoatInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateBoatInput.BoatId)
                    return BadRequest();

                await _unitOfWork.BoatCP.UpdateBoat(updateBoatInput);

                return NoContent();
            }
            catch (DataValidationException dataValidation)
            {
                if(dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                if(dataValidation.ExceptionType == ExceptionTypesEnum.Forbidden)
                    return StatusCode(StatusCodes.Status403Forbidden);

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // POST: api/Boats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<BoatOutputDTO>> PostBoat(AddBoatInputDTO boatInput)
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
                    user = await _unitOfWork.UserManager.FindByIdAsync(boatInput.OwnerId);
                }
                boatInput.OwnerId = user.Id;
            

                int boatId = await _unitOfWork.BoatCP.CreateBoat(boatInput);

                return CreatedAtAction("GetBoat", new { id = boatId });
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

        // PUT: api/Boats/5/approve 
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> PutApproveBoat(int id)
        {
            try
            {
                await _unitOfWork.BoatCEN.ApproveBoat(id);

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

        // PUT: api/Boats/5/disapprove 
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}/disapprove")]
        public async Task<IActionResult> PutDisapproveBoat(int id,DisapproveBoatInputDTO disapproveBoatInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                //Obtengo el id del admin por el token
                disapproveBoatInput.AdminId = User.UserId;

                await _unitOfWork.BoatCP.DisapproveBoat(id,disapproveBoatInput);

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

        //api/boats/5/resource/image
        [CustomAuthorize(UserRolesConstant.ADMIN, UserRolesConstant.BOAT_OWNER)]
        [HttpPost("{id}/resource/image")]
        public async Task<IActionResult> PostUploadImage(int id, IFormFile imageFile,bool main)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.BoatCP.AddImage(id, imageFile, main);
                
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

        //api/boats/5/resource/23
        [CustomAuthorize(UserRolesConstant.ADMIN, UserRolesConstant.BOAT_OWNER)]
        [HttpDelete("{id}/resource/{resourceId}")]
        public async Task<IActionResult> DeleteBoatImage(int id,int resourceId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.BoatCP.RemoveImage(id, resourceId,User.ApplicationUser,UserRoles);

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


        // POST: api/Boats/requiredTitles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPost("requiredTitles")]
        public async Task<ActionResult<BoatTitleOutputDTO>> PostBoatTitle(AddBoatTitleInputDTO boatTitleInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int boatTitleId = await _unitOfWork.BoatTitlesCEN.AddBoatTitle(boatTitleInput);

                return CreatedAtAction("GetBoatTitle", new { id = boatTitleId });
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

        // GET: api/Boats/requiredTitles
        [HttpGet("requiredTitles")]
        public async Task<ActionResult<GenericResponseDTO<BoatTitleOutputDTO>>> GetRequiredTitlesBoats([FromQuery] BoatTitlesFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var boatTitleTotal = await _unitOfWork.BoatTitlesCEN.GetTotal();

                var requiredTitle = (await _unitOfWork.BoatTitlesCEN.GetAll(pagination: pagination ?? new Pagination(),
                     filters: filters
                     ))
                    .Select(x => BoatTitleAssemblers.Convert(x));

                return new GenericResponseDTO<BoatTitleOutputDTO>(requiredTitle, pagination.Limit, pagination.Offset, boatTitleTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Boats/requiredTitles/list
        [HttpGet("requiredTitles/list")]
        public async Task<ActionResult<IEnumerable<BoatTitleOutputDTO>>> GetRequiredTitlesBoatsAll()
        {

            try
            {
                var boatTitles = (await _unitOfWork.BoatTitlesCEN.GetAll());

                return Ok(boatTitles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }

        }

        // PUT: api/Boats/requiredTitles
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("requiredTitles/{id}")]
        public async Task<IActionResult> PutBoatTitle(int id, UpdateBoatTitleDTO updateBoatTitle)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateBoatTitle.TitleId)
                    return BadRequest();

                await _unitOfWork.BoatTitlesCEN.UpdateBoatTitle(updateBoatTitle);

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

        // DELETE: api/Boats/requiredTitles

        [HttpDelete("requiredTitles/{id}")]
        public async Task<ActionResult<BoatTitleOutputDTO>> DeleteBoatTitle(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.BoatTitlesCEN.DeleteBoatTitle(id);

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

        // GET: api/Boats/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<BoatTypeOutputDTO>>> GetBoatTypes()
        {

            try
            {
                var boatTypes = (await _unitOfWork.BoatTypeCEN.GetAll());

                return Ok(boatTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }

        }
    }
}
