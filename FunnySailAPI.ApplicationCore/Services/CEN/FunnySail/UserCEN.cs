using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class UserCEN : IUserCEN
    {
        private readonly IUserCAD _userCAD;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserCEN(IUserCAD userCAD,
                       SignInManager<ApplicationUser> signInManager,
                       UserManager<ApplicationUser> userManager)
        {
            _userCAD = userCAD;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task LogoutUser(ApplicationUser user, LoginUserInputDTO loginUserInput)
        {
             await _signInManager.SignOutAsync();
        }

        private string ProccessIdentityError(IdentityResult result)
        {
            string errors = "";
            foreach(var error in result.Errors)
            {
                errors += $"{error.Code} => {error.Description}.{Environment.NewLine}";
            }

            return errors;
        }

        public IUserCAD GetUserCAD()
        {
            return _userCAD;
        }

        public async Task<int> GetTotal(UsersFilters filters)
        {
            var query = _userCAD.GetFiltered(filters);

            return await _userCAD.GetCounter(query);
        }

        public async Task<IList<UsersEN>> GetAll(UsersFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<UsersEN>, IOrderedQueryable<UsersEN>> orderBy = null,
            Func<IQueryable<UsersEN>, IIncludableQueryable<UsersEN, object>> includeProperties = null)
        {
            var query = _userCAD.GetFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.UserId);

            return await _userCAD.Get(query, orderBy, includeProperties, pagination);
        }


        public async Task AddRole(string id, string[] roles)
        {
            if (roles.Any(x=> !RolesHelpers.ExistRole(x)))
            {
                string rolesNotFound = String.Join(",", roles.Where(x => !RolesHelpers.ExistRole(x))
                    .ToList());
                throw new DataValidationException(rolesNotFound, rolesNotFound,
                    ExceptionTypesEnum.DontExists);
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new DataValidationException("User", "Usuario", ExceptionTypesEnum.NotFound);

            await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task DeleteRole(string id, string[] roles)
        {
            if (roles.Any(x => !RolesHelpers.ExistRole(x)))
            {
                string rolesNotFound = String.Join(",", roles.Where(x => !RolesHelpers.ExistRole(x))
                    .ToList());
                throw new DataValidationException(rolesNotFound, rolesNotFound,
                    ExceptionTypesEnum.DontExists);
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new DataValidationException("User", "Usuario", ExceptionTypesEnum.NotFound);


            await _userManager.RemoveFromRolesAsync(user, roles);
        }


        public async Task<IList<UsersEN>> GetOwnerWithInvPending()
        {
            return await _userCAD.GetOwnerWithInvPending();
        }
    }
}
