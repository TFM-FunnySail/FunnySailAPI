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
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Port;
using Microsoft.EntityFrameworkCore.Query;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IPortCEN
    {
        Task<int> AddPort(string name, string location);
        IPortCAD GetPortCAD();
        Task<PortEN> EditPort(UpdatePortDTO updatePortInput);
        Task<bool> AnyPortById(int portId);
        Task DeletePort(int id);
        Task<IList<PortEN>> GetAll(PortFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<PortEN>, IOrderedQueryable<PortEN>> orderBy = null,
            Func<IQueryable<PortEN>, IIncludableQueryable<PortEN, object>> includeProperties = null);
        Task<int> GetTotal(PortFilters filters = null);
    }
}
