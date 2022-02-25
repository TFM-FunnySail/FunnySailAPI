using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
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
                .Include(x => x.Active)
                .Include(x => x.ActivityDate)
                .Include(x => x.Description)
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

            if (activityFilters.PriceRange?.MinPrice != null)
                activities = activities.Where(x => x.Price >= activityFilters.PriceRange.MinPrice);

            if (activityFilters.PriceRange?.MaxPrice != null)
                activities = activities.Where(x => x.Price < activityFilters.PriceRange.MaxPrice);

            if (activityFilters.ActivityDateRange?.InitialDate != null)
                activities = activities.Where(x => x.ActivityDate >= activityFilters.ActivityDateRange.InitialDate);

            if (activityFilters.ActivityDateRange?.EndDate != null)
                activities = activities.Where(x => x.ActivityDate < activityFilters.ActivityDateRange.EndDate);

            if (activityFilters.ActivityIdList?.Count > 0)
                activities = activities.Where(x => activityFilters.ActivityIdList.Contains(x.Id));

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
