using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ActivityBookingCAD : BaseCAD<ActivityBookingEN>, IActivityBookingCAD
    {
        public ActivityBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ActivityBookingEN> FindByIds(int idActivity, int idBooking)
        {
            return await _dbContext.ActivityBookings.FindAsync(idActivity, idBooking);
        }

        public IQueryable<ActivityBookingEN> GetActivityBookingFiltered(ActivityBookingFilters filters)
        {
            IQueryable<ActivityBookingEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.ActivityId != 0)
                query = query.Where(x => x.ActivityId == filters.ActivityId);

            if (filters.RangePrice != (null,null))
                query = query.Where(x => filters.RangePrice.Item1 <= x.Price && x.Price <= filters.RangePrice.Item2);


            return query;
        }
    }
}
