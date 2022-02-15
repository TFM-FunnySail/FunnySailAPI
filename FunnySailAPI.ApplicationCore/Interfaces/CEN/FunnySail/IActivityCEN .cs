using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IActivityCEN
    {
        Task<int> AddActivity(DateTime activityDate, string name, decimal price, string description);
        IActivityCAD GetActivityCAD();
        Task<ActivityEN> EditActivity(int activityId, DateTime activityDate, string name, decimal price, string description);
        Task<ActivityEN> DeactivateActivity(int activityId);
    }
}
