using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRequestUtilityService _requestUtilityService;
        public AccountController(IAccountService accountService,
                                 IRequestUtilityService requestUtilityService)
        {
            _accountService = accountService;
            _requestUtilityService = requestUtilityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateResponseDTO>> Login(LoginUserInputDTO loginUserInput)
        {
            try
            {
                AuthenticateResponseDTO response = await _accountService.LoginUser(loginUserInput,
                    _requestUtilityService.ipAddress(Request, HttpContext));
                setTokenCookie(response.RefreshToken);
                return Ok(response);
            }
            catch (DataValidationException dataValidation)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity,
                    new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO(ex));
            }
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
