using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Sercices;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IServiceCEN
    {
        Task<int> CreateService(string name, decimal price, string description);
        Task<int> CreateService(AddServiceDTO serviceInput);
        Task<ServiceEN> UpdateService(UpdateServiceDTO updateServiceInput);
        Task DeleteService(int id);
        IServiceCAD GetServiceCAD();
        Task<IList<ServiceEN>> GetAll(ServiceFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ServiceEN>, IOrderedQueryable<ServiceEN>> orderBy = null,
        Func<IQueryable<ServiceEN>, IIncludableQueryable<ServiceEN, object>> includeProperties = null);
        Task<int> GetTotal(ServiceFilters filters = null);

    }
}
