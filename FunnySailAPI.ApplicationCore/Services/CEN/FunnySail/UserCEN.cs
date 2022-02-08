using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class UserCEN : IUserCEN
    {
        private readonly IUserCAD _userCAD;
        private readonly UserManager<ApplicationUser> _userManager;
        private IDatabaseTransactionFactory _databaseTransactionFactory;
        private readonly IEmailSender _emailSender;

        public UserCEN(IUserCAD userCAD,
                       UserManager<ApplicationUser> userManager,
                       IDatabaseTransactionFactory databaseTransactionFactory,
                       IEmailSender emailSender)
        {
            _userCAD = userCAD;
            _userManager = userManager;
            _databaseTransactionFactory = databaseTransactionFactory;
            _emailSender = emailSender;
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
                        Name = addUserInput.Name,
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
    }
}
