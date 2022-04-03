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
using FunnySailAPI.Helpers;
using FunnySailAPI.ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using FunnySailAPI.ApplicationCore.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IRequestUtilityService _requestUtilityService;

        public UsersController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService,
                               IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
            _emailSender = emailSender;
        }

        // GET: api/Users
        [CustomAuthorize(UserRolesConstant.ADMIN)]
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

                ApplicationUser user;
                if (!RolesHelpers.AnyRole(UserRoles, UserRolesConstant.ADMIN))
                {
                    if (id != User.UserId)
                        return BadRequest();

                    user = User.ApplicationUser;
                }
                else
                {
                    user = await _unitOfWork.UserManager.FindByEmailAsync(userInput.Email);
                }

                await _unitOfWork.UserCP.EditUser(user, userInput);

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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserOutputDTO>> PostUsersEN(AddUserInputDTO addUserInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();


                (IdentityResult result, ApplicationUser user) = await _unitOfWork.UserCP.CreateUser(addUserInput);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ErrorResponseDTO("User could not be created", 
                        "El usuario no pudo ser creado"));

                var code = await _unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return CreatedAtAction("GetUsersEN", new { id = user.Id });
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

        // POST: api/Users/5/role
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPost("{id}/role")]
        public async Task<IActionResult> PostRole(string id,[FromBody]string[] roles)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.UserCEN.AddRole(id, roles);

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

        // DEL: api/Users/5/role
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpDelete("{id}/role")]
        public async Task<IActionResult> DeleteRole(string id, [FromBody] string[] roles)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.UserCEN.DeleteRole(id, roles);

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
