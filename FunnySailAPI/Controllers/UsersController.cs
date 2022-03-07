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
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output.User;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<UserOutputDTO>>> GetUsersInfo([FromQuery] UsersFilters filters,[FromQuery] Pagination pagination)
        {
            try
            {
                int total = await _unitOfWork.UserCEN.GetTotal(filters);

                var users = (await _unitOfWork.UserCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.ApplicationUser)))
                    .Select(x => UserAssemblers.Convert(x));

                return new GenericResponseDTO<UserOutputDTO>(users, pagination.Limit, pagination.Offset, total);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserOutputDTO>> GetUsersEN(string id)
        {
            try
            {
                var users = await _unitOfWork.UserCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new UsersFilters
                {
                    UserId = id
                }, includeProperties: source => source.Include(x => x.ApplicationUser));

                var user = users.Select(x => UserAssemblers.Convert(x)).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersEN(string id, AddUserInputDTO userInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                //if (id != idToken)
                //    return BadRequest();

                //Falta el usuario que se obtiene por el token
                await _unitOfWork.UserCEN.EditUser(null,userInput);

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

        //// POST: api/Users
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<UsersEN>> PostUsersEN(UsersEN usersEN)
        //{
        //    _context.UsersInfo.Add(usersEN);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UsersENExists(usersEN.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUsersEN", new { id = usersEN.UserId }, usersEN);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<UsersEN>> DeleteUsersEN(string id)
        //{
        //    var usersEN = await _context.UsersInfo.FindAsync(id);
        //    if (usersEN == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UsersInfo.Remove(usersEN);
        //    await _context.SaveChangesAsync();

        //    return usersEN;
        //}

    }
}
