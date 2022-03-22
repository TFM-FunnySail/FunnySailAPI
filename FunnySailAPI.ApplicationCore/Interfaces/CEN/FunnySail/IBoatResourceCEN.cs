using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
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
    public interface IBoatResourceCEN
    {
        Task<(int, int)> AddBoatResource(BoatResourceEN boatResourceEN);
        Task<IList<BoatResourceEN>> GetAll(BoatResourceFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatResourceEN>, IOrderedQueryable<BoatResourceEN>> orderBy = null,
            Func<IQueryable<BoatResourceEN>, IIncludableQueryable<BoatResourceEN, object>> includeProperties = null);
        IBoatResourceCAD GetBoatResourceCAD();
    }
}
