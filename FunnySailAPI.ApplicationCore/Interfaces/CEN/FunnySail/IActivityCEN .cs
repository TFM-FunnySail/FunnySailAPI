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

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IActivityCEN
    {
        Task<int> AddActivity(AddActivityInputDTO addActivityInput);
        IActivityCAD GetActivityCAD();
        Task<ActivityEN> EditActivity(UpdateAcitivityDTO updateAcitivityInput);
        Task<ActivityEN> DeactivateActivity(int activityId);
        Task<List<ActivityEN>> GetAvailableActivity(Pagination pagination, DateTime? initialDate, DateTime? endDate, decimal? minPrice, decimal? maxPrice, String name);
    }
}
