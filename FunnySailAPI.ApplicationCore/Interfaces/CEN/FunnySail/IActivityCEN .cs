using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Activity;
using Microsoft.EntityFrameworkCore.Query;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IActivityCEN
    {
        Task<int> AddActivity(AddActivityInputDTO addActivityInput);
        IActivityCAD GetActivityCAD();
        Task<ActivityEN> EditActivity(UpdateAcitivityInputDTO updateAcitivityInput);
        Task<ActivityEN> ActivateActivity(int activityId);
        Task<ActivityEN> DeactivateActivity(int activityId);
        Task<IList<ActivityEN>> GetAvailableActivities(Pagination pagination, DateTime initialDate, DateTime endDate,
           Func<IQueryable<ActivityEN>, IOrderedQueryable<ActivityEN>> orderBy = null,
           Func<IQueryable<ActivityEN>, IIncludableQueryable<ActivityEN, object>> includeProperties = null);

        Task<IList<ActivityEN>> GetAll(ActivityFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<ActivityEN>, IOrderedQueryable<ActivityEN>> orderBy = null,
            Func<IQueryable<ActivityEN>, IIncludableQueryable<ActivityEN, object>> includeProperties = null);
        Task<int> GetTotal(ActivityFilters filters = null);
    }
}
