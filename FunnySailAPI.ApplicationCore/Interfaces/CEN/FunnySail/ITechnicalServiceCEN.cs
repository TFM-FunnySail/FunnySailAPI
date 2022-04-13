using FunnySailAPI.ApplicationCore.Models.DTO.Input;
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
    public interface ITechnicalServiceCEN
    {
        Task<int> AddTechnicalService(decimal price, string description);
        Task<TechnicalServiceEN> UpdateTechnicalService(UpdateTechnicalServiceDTO updateServiceInput);
        Task DeleteService(int id);
        Task<int> AddTechnicalServiceBoat(ScheduleTechnicalServiceDTO scheduleTechnicalService);
        Task<IList<TechnicalServiceEN>> GetAll(TechnicalServiceFilters filters = null,
       Pagination pagination = null,
       Func<IQueryable<TechnicalServiceEN>, IOrderedQueryable<TechnicalServiceEN>> orderBy = null,
       Func<IQueryable<TechnicalServiceEN>, IIncludableQueryable<TechnicalServiceEN, object>> includeProperties = null);
        Task<int> GetTotal(TechnicalServiceFilters filters = null);
        Task<TechnicalServiceEN> CancelTechnicalService(int technicalServiceId);
    }
}
