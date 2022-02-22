using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
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
        Task<BoatEN> ApproveBoat(int boatId);
        Task<BoatEN> DisapproveBoat(int boatId);
        IBoatCAD GetBoatCAD();
        Task<List<BoatEN>> GetAvailableBoats(Pagination pagination, DateTime initialDate, DateTime endDate);
        Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput);
    }
}
