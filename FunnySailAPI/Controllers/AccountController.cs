using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Extensions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
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
    public class AccountController : BaseController
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
                setTokenCookie(response);
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

        [HttpPost("admin/login")]
        public async Task<ActionResult<AuthenticateResponseDTO>> AdminLogin(LoginUserInputDTO loginUserInput)
        {
            try
            {
                AuthenticateResponseDTO response = await _accountService.LoginUser(loginUserInput,
                    _requestUtilityService.ipAddress(Request, HttpContext),true);
                setTokenCookie(response);
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

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponseDTO>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                AuthenticateResponseDTO response = await _accountService.RefreshToken(refreshToken,
                    _requestUtilityService.ipAddress(Request, HttpContext));
                setTokenCookie(response);
                
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

        [CustomAuthorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenInputDTO revokeTokenInput)
        {
            try
            {
                // accept token from request body or cookie
                var token = revokeTokenInput.Token ?? Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { message = "Token is required" });

                // users can revoke their own tokens and admins can revoke any tokens
                if (!(await _accountService.IsOwnsToken(User, token)) &&
                    UserRoles.Contains(UserRolesConstant.ADMIN))
                    return Unauthorized(new { message = "Unauthorized" });

                await _accountService.RevokeToken(token, _requestUtilityService.ipAddress(Request, HttpContext));
                return Ok(new { message = "Token revoked" });
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

        [CustomAuthorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordInput)
        {
            try
            {
                await _accountService.ChangePassword(User.ApplicationUser,changePasswordInput);
                return NoContent();
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


        private void setTokenCookie(AuthenticateResponseDTO response)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(TokenInfoConstant.refreshTokenExpiresInDays)
            };
            Response.Cookies.Append("refreshToken", response.JwtToken, cookieOptions);
            var expires = (DateTimeOffset)cookieOptions.Expires;
            response.RefreshTokenExpiresIn = expires.ToUnixLongTimeStamp();
        }
    }
}
