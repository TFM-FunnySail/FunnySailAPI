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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityCEN : IActivityCEN
    {
        private readonly IActivityCAD _activityCAD;

        public ActivityCEN(IActivityCAD activityCAD)
        {
            _activityCAD = activityCAD;
        }

        public async Task<int> AddActivity(AddActivityInputDTO addActivityInput)
        {
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

        public async Task<ActivityEN> EditActivity(UpdateAcitivityDTO updateAcitivityInput)
        {

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

        public async Task<List<ActivityEN>> GetAvailableActivity(Pagination pagination, DateTime? initialDate, DateTime? endDate, decimal? minPrice, decimal? maxPrice, String name)
        {

            IQueryable<ActivityEN> activitys = _activityCAD.GetActivityFiltered(new ActivityFilters
            {
                Active = true,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                InitialDate = initialDate,
                EndDate = endDate,
                Name = name
            });

            return await _activityCAD.GetAll(activitys.OrderBy(x => x.Id), pagination);
        }
    }
}
