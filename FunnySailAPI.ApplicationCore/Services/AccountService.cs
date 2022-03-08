using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
