using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatTypeCEN
    {
        Task<bool> AnyBoatTypeById(int boatTypeId);
        Task<IList<BoatTypeEN>> GetAll(BoatTypesFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatTypeEN>, IOrderedQueryable<BoatTypeEN>> orderBy = null,
            Func<IQueryable<BoatTypeEN>, IIncludableQueryable<BoatTypeEN, object>> includeProperties = null);
    }
}
