using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
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
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public UserCEN(IUserCAD userCAD,
                       SignInManager<ApplicationUser> signInManager,
                       UserManager<ApplicationUser> userManager,    
                       IDatabaseTransactionFactory databaseTransactionFactory)
        {
            _userCAD = userCAD;
            _signInManager = signInManager;
            _userManager = userManager;
            _databaseTransactionFactory = databaseTransactionFactory;
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user,AddUserInputDTO addUserInput)
        {
            user.PhoneNumber = addUserInput.PhoneNumber;

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, addUserInput.Password);

                    if (!result.Succeeded)
                    {
                        await databaseTransaction.RollbackAsync();
                        return result; 
                    }

                    UsersEN userInfo = await _userCAD.AddAsync(new UsersEN { 
                        UserId = user.Id,
                        BirthDay = addUserInput.BirthDay,
                        BoatOwner = false,
                        FirstName = addUserInput.FirstName,
                        LastName = addUserInput.LastName,
                        ReceivePromotion = addUserInput.ReceivePromotion ?? false,
                    });

                    //await _userManager.AddToRoleAsync(user, Globals.UserRoles[addUserInput.UserRole]);

                    await databaseTransaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<IdentityResult> EditUser(ApplicationUser user, AddUserInputDTO addUserInput)
        {
            user.PhoneNumber = addUserInput.PhoneNumber;
            user.Email = addUserInput.Email;

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        await databaseTransaction.RollbackAsync();
                        return result;
                    }

                    UsersEN userInfo = await _userCAD.FindById(user.Id);

                    userInfo.BirthDay = addUserInput.BirthDay;
                    userInfo.FirstName = addUserInput.FirstName;
                    userInfo.LastName = addUserInput.LastName;
                    userInfo.ReceivePromotion = addUserInput.ReceivePromotion ?? false;

                    await _userCAD.Update(userInfo);

                    //await _userManager.AddToRoleAsync(user, Globals.UserRoles[addUserInput.UserRole]);

                    await databaseTransaction.CommitAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<IdentityResult> LoginUser(ApplicationUser user, LoginUserInputDTO loginUserInput)
        {

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(loginUserInput.Email, loginUserInput.Password, loginUserInput.RememberMe, lockoutOnFailure: false);

                    if (!result.Succeeded)
                    {
                        await databaseTransaction.RollbackAsync();
                        return null;
                    }

                    await databaseTransaction.CommitAsync();

                    return null;
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<IdentityResult> LogoutUser(ApplicationUser user, LoginUserInputDTO loginUserInput)
        {

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    await _signInManager.SignOutAsync();

                    await databaseTransaction.CommitAsync();

                    return null;
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
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
    }
}
