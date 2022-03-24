using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityBookingCEN : IActivityBookingCEN
    {
        private readonly IActivityBookingCAD _activityBookingCAD;

        public ActivityBookingCEN(IActivityBookingCAD activityBookingCAD)
        {
            _activityBookingCAD = activityBookingCAD;
        }

        public async Task<Tuple<int, int>> CreateActivityBooking(ActivityBookingEN activityBookingEN)
        {
            activityBookingEN = await _activityBookingCAD.AddAsync(activityBookingEN);

            return new Tuple<int, int>(activityBookingEN.ActivityId, activityBookingEN.BookingId);
        }

        public IActivityBookingCAD GetActivityBookingCAD()
        {
            return _activityBookingCAD;
        }

        public async Task<IList<ActivityBookingEN>> GetAll(ActivityBookingFilters filters = null, Pagination pagination = null, Func<IQueryable<ActivityBookingEN>, IOrderedQueryable<ActivityBookingEN>> orderBy = null, Func<IQueryable<ActivityBookingEN>, IIncludableQueryable<ActivityBookingEN, object>> includeProperties = null)
        {
            var query = _activityBookingCAD.GetActivityBookingFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.ActivityId);

            return await _activityBookingCAD.Get(query, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(ActivityBookingFilters filters = null)
        {
            var query = _activityBookingCAD.GetActivityBookingFiltered(filters);

            return await _activityBookingCAD.GetCounter(query);
        }
    }
}
