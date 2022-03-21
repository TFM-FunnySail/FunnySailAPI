using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Activity;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityCEN : IActivityCEN
    {
        private readonly IActivityCAD _activityCAD;
        private readonly string _enName;
        private readonly string _esName;

        public ActivityCEN(IActivityCAD activityCAD)
        {
            _activityCAD = activityCAD;
            _enName = "Activity";
            _esName = "Actividad";
        }

        public async Task<int> AddActivity(AddActivityInputDTO addActivityInput)
        {
            if (addActivityInput.Name == null)
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);


            ActivityEN dbActivity = await _activityCAD.AddAsync(new ActivityEN
            {
                ActivityDate = addActivityInput.ActivityDate,
                Name = addActivityInput.Name,
                Price = addActivityInput.Price,
                Description = addActivityInput.Description,
                Active = addActivityInput.Active
            });

            return dbActivity.Id;
        }

        public async Task<ActivityEN> EditActivity(UpdateAcitivityInputDTO updateAcitivityInput)
        {
            if (updateAcitivityInput.Name == null)
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);

            ActivityEN activity = await _activityCAD.FindById(updateAcitivityInput.Id);

            if (activity == null)
                throw new DataValidationException("Activity", "Actividad",
                    ExceptionTypesEnum.NotFound);

            activity.ActivityDate = updateAcitivityInput.ActivityDate;
            activity.Name = updateAcitivityInput.Name;
            activity.Price = updateAcitivityInput.Price;
            activity.Description = updateAcitivityInput.Description;
            activity.Active = updateAcitivityInput.Active;

            await _activityCAD.Update(activity);

            return activity;
        }

        public async Task<ActivityEN> ActivateActivity(int activityId)
        {
            ActivityEN activity = await _activityCAD.FindById(activityId);

            activity.Active = true;

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

        public async Task<IList<ActivityEN>> GetAvailableActivities(Pagination pagination, DateTime initialDate, DateTime endDate,
           Func<IQueryable<ActivityEN>, IOrderedQueryable<ActivityEN>> orderBy = null,
           Func<IQueryable<ActivityEN>, IIncludableQueryable<ActivityEN, object>> includeProperties = null)
        {
            List<int> idsNotAvailable = await _activityCAD.GetActivityIdsNotAvailable(initialDate, endDate);

            IQueryable<ActivityEN> activities = _activityCAD.GetActivityFiltered(new ActivityFilters
            {
                Active = true
            });

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _activityCAD.Get(activities, orderBy, includeProperties, pagination);
        }

        public async Task<IList<ActivityEN>> GetAll(ActivityFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ActivityEN>, IOrderedQueryable<ActivityEN>> orderBy = null,
        Func<IQueryable<ActivityEN>, IIncludableQueryable<ActivityEN, object>> includeProperties = null)
        {
            var activities = _activityCAD.GetActivityFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _activityCAD.Get(activities, orderBy, includeProperties, pagination);
        }
        public async Task<int> GetTotal(ActivityFilters filters = null)
        {
            var activities = _activityCAD.GetActivityFiltered(filters);

            return await _activityCAD.GetCounter(activities);
        }
    }
}
