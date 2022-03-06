using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IMooringCAD : IBaseCAD<MooringEN>
    {
        Task<MooringEN> FindByIdAllData(int id, bool port = false);
        Task<bool> AnyBoatWithMooring(int mooringId);
        IQueryable<MooringEN> GetFiltered(MooringFilters filters);
    }
}
