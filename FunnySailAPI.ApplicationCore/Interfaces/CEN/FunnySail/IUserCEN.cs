using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Account;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IUserCEN
    {
        IUserCAD GetUserCAD();
        Task LogoutUser(ApplicationUser user, LoginUserInputDTO loginUserInput);
        Task<int> GetTotal(UsersFilters filters);
        Task<IList<UsersEN>> GetAll(UsersFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<UsersEN>, IOrderedQueryable<UsersEN>> orderBy = null,
            Func<IQueryable<UsersEN>, IIncludableQueryable<UsersEN, object>> includeProperties = null);
        Task AddRole(string id, string[] roles);
        Task DeleteRole(string id, string[] roles);
        Task<IList<UsersEN>> GetOwnerWithInvPending();
    }
}
