using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityResourceCEN : IActivityResourceCEN
    {
        private readonly IActivityResourcesCAD _activityResourcesCAD;

        public ActivityResourceCEN(IActivityResourcesCAD activityResourcesCAD)
        {
            _activityResourcesCAD = activityResourcesCAD;
        }

        public async Task<(int, int)> AddActivityResource(ActivityResourcesEN activityResourcesEN)
        {
            if (activityResourcesEN.ActivityId == 0)
                throw new DataValidationException("Activity id", "Id de la actividad", ExceptionTypesEnum.IsRequired);

            activityResourcesEN = await _activityResourcesCAD.AddAsync(activityResourcesEN);

            return (activityResourcesEN.ActivityId, activityResourcesEN.ResourceId);
        }
    }
}
