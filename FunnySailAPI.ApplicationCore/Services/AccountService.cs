using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.DTO.Output.Account;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountService(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager; 
            _userManager = userManager;
        }


        public async Task<AuthenticateResponseDTO> LoginUser(LoginUserInputDTO loginUserInput,
                                                             string ipAddress)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginUserInput.Email, loginUserInput.Password, true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new DataValidationException("Invalid username or password.",
                    "Usuario o contraseña inválida.");
            
            return new AuthenticateResponseDTO
              {

            };
        }
    }
}
