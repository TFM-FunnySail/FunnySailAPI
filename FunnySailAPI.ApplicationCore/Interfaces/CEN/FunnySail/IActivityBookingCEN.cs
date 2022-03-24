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
    public interface IActivityBookingCEN
    {
        Task<Tuple<int, int>> CreateActivityBooking(ActivityBookingEN activityBookingEN);
        IActivityBookingCAD GetActivityBookingCAD();

        Task<IList<ActivityBookingEN>> GetAll(ActivityBookingFilters filters = null,
                                   Pagination pagination = null,
                                   Func<IQueryable<ActivityBookingEN>, IOrderedQueryable<ActivityBookingEN>> orderBy = null,
                                   Func<IQueryable<ActivityBookingEN>, IIncludableQueryable<ActivityBookingEN, object>> includeProperties = null);
        Task<int> GetTotal(ActivityBookingFilters filters = null);
    }
}
