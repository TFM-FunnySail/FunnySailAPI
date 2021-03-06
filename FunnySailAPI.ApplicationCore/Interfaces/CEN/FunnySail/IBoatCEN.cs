using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatCEN
    {
        Task<int> CreateBoat(BoatEN boatEN);
        Task<BoatEN> ApproveBoat(int boatId);
        Task<BoatEN> DisapproveBoat(int boatId);
        IBoatCAD GetBoatCAD();
        Task<IList<BoatEN>> GetAvailableBoats(Pagination pagination, DateTime initialDate, DateTime endDate,
            Func<IQueryable<BoatEN>, IOrderedQueryable<BoatEN>> orderBy = null,
            Func<IQueryable<BoatEN>, IIncludableQueryable<BoatEN, object>> includeProperties = null); Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput);
        Task<IList<BoatEN>> GetAll(BoatFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatEN>, IOrderedQueryable<BoatEN>> orderBy = null,
            Func<IQueryable<BoatEN>, IIncludableQueryable<BoatEN, object>> includeProperties = null);
        Task<int> GetTotal(BoatFilters filters = null);
        Task<int> GetAvailableBoatsTotal(DateTime initialDate, DateTime endDate);
    }
}
