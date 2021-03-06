using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IUserCAD : IBaseCAD<UsersEN>
    {
        IQueryable<UsersEN> GetFiltered(UsersFilters filters);
        Task<IList<UsersEN>> GetOwnerWithInvPending();
    }
}
