using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IUserCP
    {
        Task<(IdentityResult, ApplicationUser)> CreateUser(AddUserInputDTO addUserInput);
        Task<IdentityResult> EditUser(ApplicationUser user, EditUserInputDTO addUserInput);

    }
}
