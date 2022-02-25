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
    public interface IBoatCAD : IBaseCAD<BoatEN>
    {
        Task<BoatEN> FindByIdAllData(int boatId);
        IQueryable<BoatEN> GetBoatFiltered(BoatFilters boatFilters);
        Task<List<int>> GetBoatIdsNotAvailable(DateTime initialDate, DateTime endDate);
        Task<bool> AnyById(int boatID);
        Task<bool> IsBoatBusy(int boatId, DateTime serviceDate);
        Task<List<int>> GetBoatIdsNotAvailable(DateTime initialDate, DateTime endDate, List<int> ids);
        Task<List<BoatEN>> GetBoatFilteredList(BoatFilters boatFilters);
    }
}
