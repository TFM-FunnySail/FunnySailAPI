using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IServiceCAD : IBaseCAD<ServiceEN>
    {
        Task<ServiceEN> FindByIdAllData(int serviceId);
        Task<List<int>> GetServiceIdsNotAvailable(DateTime initialDate, DateTime endDate);
        Task<List<int>> GetServiceIdsNotAvailable(DateTime initialDate, DateTime endDate, List<int> ids);
        IQueryable<ServiceEN> GetServiceFiltered(ServiceFilters serviceFilters);
        Task<List<ServiceEN>> GetServiceFilteredList(ServiceFilters serviceFilters);
    }
}
