using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityCEN : IActivityCEN
    {
        private readonly IActivityCAD _activityCAD;

        public ActivityCEN(IActivityCAD activityCAD)
        {
            _activityCAD = activityCAD;
        }

        public async Task<int> AddActivity(DateTime activityDate, string name, decimal price, string description)
        {
            ActivityEN dbActivity = await _activityCAD.AddAsync(new ActivityEN
            {
                ActivityDate = activityDate,
                Name = name,
                Price = price,
                Description = description
            });

            return dbActivity.Id;
        }

        public async Task<ActivityEN> EditActivity(int activityId, DateTime activityDate, string name, decimal price, string description)
        {

            ActivityEN activity = await _activityCAD.FindById(activityId);

            activity.ActivityDate = activityDate;
            activity.Name = name;
            activity.Price = price;
            activity.Description = description;

            await _activityCAD.Update(activity);

            return activity;
        }

        public async Task<ActivityEN> DeactivateActivity(int activityId)
        {
            ActivityEN activity = await _activityCAD.FindById(activityId);

            activity.Active = false;

            await _activityCAD.Update(activity);

            return activity;
        }

        public IActivityCAD GetActivityCAD()
        {
            return _activityCAD;
        }

        public async Task<List<ActivityEN>> GetAvailableActivity(Pagination pagination, DateTime initialDate, DateTime endDate)
        {

            IQueryable<ActivityEN> activitys = _activityCAD.GetActivityFiltered(new ActivityFilters
            {
                Active = true
            });

            return await _activityCAD.GetAll(activitys.OrderBy(x => x.Id), pagination);
        }
    }
}
