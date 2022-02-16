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
        IQueryable<ActivityEN> GetActivityFiltered(ActivityFilters activityFilters);
    }
}
