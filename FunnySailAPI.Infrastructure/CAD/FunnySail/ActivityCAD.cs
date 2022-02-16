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

        public IQueryable<ActivityEN> GetActivityFiltered(ActivityFilters activityFilters)
        {
            IQueryable<ActivityEN> activitys = GetIQueryable();

            if (activityFilters == null)
                return activitys;

            if (activityFilters.ActivityId != 0)
                activitys = activitys.Where(x => x.Id == activityFilters.ActivityId);

            if (activityFilters.Active != null)
                activitys = activitys.Where(x => x.Active == activityFilters.Active);

            if (activityFilters.PriceRange?.MinPrice != null)
                activitys = activitys.Where(x => x.Price >= activityFilters.PriceRange.MinPrice);

            if (activityFilters.PriceRange?.MaxPrice != null)
                activitys = activitys.Where(x => x.Price < activityFilters.PriceRange.MaxPrice);

            if (activityFilters.ActivityDateRange?.InitialDate != null)
                activitys = activitys.Where(x => x.ActivityDate >= activityFilters.ActivityDateRange.InitialDate);

            if (activityFilters.ActivityDateRange?.EndDate != null)
                activitys = activitys.Where(x => x.ActivityDate < activityFilters.ActivityDateRange.EndDate);

            return activitys;
        }
    }
}
