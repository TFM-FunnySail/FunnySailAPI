using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IActivityCAD : IBaseCAD<ActivityEN>
    {

        Task<ActivityEN> FindByIdAllData(int activityId);
        Task<List<int>> GetActivityIdsNotAvailable(DateTime initialDate, DateTime endDate);
        Task<List<int>> GetActivityIdsNotAvailable(DateTime initialDate, DateTime endDate, List<int> ids);
        IQueryable<ActivityEN> GetActivityFiltered(ActivityFilters activityFilters);
        Task<List<ActivityEN>> GetServiceFilteredList(ActivityFilters activityFilters);

    }
}
