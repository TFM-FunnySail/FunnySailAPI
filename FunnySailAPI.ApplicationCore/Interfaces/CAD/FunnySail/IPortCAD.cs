using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IPortCAD : IBaseCAD<PortEN>
    {
        Task<PortEN> FindByIdAllData(int bookingId);
        IQueryable<PortEN> GetPortFiltered(PortFilters filters);
        Task<bool> AnyBoatInPort(int id);
    }
}
