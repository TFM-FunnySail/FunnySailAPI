using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IUserCEN
    {
        Task<IdentityResult> CreateUser(ApplicationUser user, AddUserInputDTO addUserInput);
        IUserCAD GetUserCAD();
        Task<IdentityResult> EditUser(ApplicationUser user, AddUserInputDTO addUserInput);
        Task<IdentityResult> LoginUser(ApplicationUser user, LoginUserInputDTO loginUserInput);
        Task<IdentityResult> LogoutUser(ApplicationUser user, LoginUserInputDTO loginUserInput);
    }
}
