using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class UserCP : IUserCP
    {
        private readonly IUserCEN _userCEN;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public UserCP(IUserCEN userCEN,
                       SignInManager<ApplicationUser> signInManager,
                       UserManager<ApplicationUser> userManager,
                       IDatabaseTransactionFactory databaseTransactionFactory)
        {
            _userCEN = userCEN;
            _signInManager = signInManager;
            _userManager = userManager;
            _databaseTransactionFactory = databaseTransactionFactory;
        }

        public async Task<(IdentityResult, ApplicationUser)> CreateUser(AddUserInputDTO addUserInput)
        {
            var user = new ApplicationUser
            {
                Email = addUserInput.Email,
                UserName = addUserInput.Email,
                PhoneNumber = addUserInput.PhoneNumber,
            };

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, addUserInput.Password);

                    if (!result.Succeeded)
                    {
                        await databaseTransaction.RollbackAsync();
                        return (result, user);
                    }

                    UsersEN userInfo = await _userCEN.GetUserCAD().AddAsync(new UsersEN
                    {
                        UserId = user.Id,
                        BirthDay = addUserInput.BirthDay,
                        BoatOwner = false,
                        FirstName = addUserInput.FirstName,
                        LastName = addUserInput.LastName,
                        ReceivePromotion = addUserInput.ReceivePromotion ?? false,
                    });

                    await _userManager.AddToRoleAsync(user, UserRolesConstant.CLIENT);

                    await databaseTransaction.CommitAsync();

                    return (result, user);
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }
        public async Task<IdentityResult> EditUser(ApplicationUser user, EditUserInputDTO addUserInput)
        {
            user.PhoneNumber = addUserInput.PhoneNumber;

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

                    UsersEN userInfo = await _userCEN.GetUserCAD().FindById(user.Id);

                    userInfo.BirthDay = addUserInput.BirthDay;
                    userInfo.FirstName = addUserInput.FirstName;
                    userInfo.LastName = addUserInput.LastName;
                    userInfo.ReceivePromotion = addUserInput.ReceivePromotion ?? false;

                    await _userCEN.GetUserCAD().Update(userInfo);

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
    }
}
