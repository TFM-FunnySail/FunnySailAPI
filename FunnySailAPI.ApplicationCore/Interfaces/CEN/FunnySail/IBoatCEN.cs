using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatCEN
    {
        Task<int> CreateBoat(BoatEN boatEN);
        Task<BoatEN> GetAllDataBoat(int boatId);
        Task<BoatEN> ApproveBoat(int boatId);
        Task<BoatEN> DisapproveBoat(int boatId);
        IBoatCAD GetBoatCAD();
        IQueryable<BoatEN> FilterBoat(BoatFiltersDTO boatFilters);
    }
}
