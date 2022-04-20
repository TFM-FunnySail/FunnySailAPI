using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBoatTitleCAD : IBaseCAD<BoatTitlesEN>
    {
        Task<bool> AnyBoatWithTitle(int titleId);
        Task<BoatTitlesEN> FindByIdAllData(int id, bool requiredTitle = false);
        IQueryable<BoatTitlesEN> GetRequiredTitleFiltered(BoatTitlesFilters requiredTitlesFilters);
    }
}
