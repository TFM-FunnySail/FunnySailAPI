using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
 
    public class ActivityCAD : BaseCAD<ActivityEN>, IActivityCAD
    {
        public ActivityCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ActivityEN> FindByIdAllData(int activityId)
        {
            return await _dbContext.Activity
                .FirstOrDefaultAsync(x => x.Id == activityId);
        }

        public async Task<List<int>> GetActivityIdsNotAvailable(DateTime initialDate, DateTime endDate)
        {
            return await _dbContext.Bookings.
          Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
          (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
          .Join(_dbContext.ActivityBookings,
          b => b.Id, ab => ab.BookingId,
          (booking, activityBooking) => activityBooking.ActivityId)
          .Distinct().ToListAsync();
        }

        public async Task<List<int>> GetActivityIdsNotAvailable(DateTime initialDate, DateTime endDate, List<int> ids)
        {
            return await _dbContext.Bookings.
               Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
               (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
               .Join(_dbContext.ActivityBookings.Where(x => ids.Contains(x.ActivityId)),
               b => b.Id, ab => ab.BookingId,
               (booking, activityBooking) => activityBooking.ActivityId)
               .Distinct().ToListAsync();
        }

        #region Filter
        public IQueryable<ActivityEN> GetActivityFiltered(ActivityFilters activityFilters)
        {
            IQueryable<ActivityEN> activities = GetIQueryable();

            if (activityFilters == null)
                return activities;

            if (activityFilters.ActivityId != 0)
                activities = activities.Where(x => x.Id == activityFilters.ActivityId);
            
            if (activityFilters.Active != null)
                activities = activities.Where(x => x.Active == activityFilters.Active);
            
            if (activityFilters.MinPrice != 0)
                activities = activities.Where(x => x.Price >= activityFilters.MinPrice);

            if (activityFilters.MaxPrice != 0)
                activities = activities.Where(x => x.Price < activityFilters.MaxPrice);
            
            if (activityFilters.Name != null)
                activities = activities.Where(x => x.Name.Contains(activityFilters.Name));

            if (activityFilters.Description != null)
                activities = activities.Where(x => x.Description.Contains(activityFilters.Description));

            if (activityFilters.ActivityIdList?.Count > 0)
                activities = activities.Where(x => activityFilters.ActivityIdList.Contains(x.Id));

            if (activityFilters.ActivityNotIdList?.Count > 0)
                activities = activities.Where(x => !activityFilters.ActivityNotIdList.Contains(x.Id));

            return activities;
        }

        public async Task<List<ActivityEN>> GetServiceFilteredList(ActivityFilters activityFilters)
        {
            IQueryable<ActivityEN> activities = GetActivityFiltered(activityFilters);

            return await activities.ToListAsync();
        }
        #endregion
    }
}
