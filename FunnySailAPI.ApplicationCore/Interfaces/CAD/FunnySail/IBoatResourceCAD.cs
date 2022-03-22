using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBoatResourceCAD : IBaseCAD<BoatResourceEN>
    {
        Task<(int, int)> AddBoatResource(int boatId, int resourceId);
        IQueryable<BoatResourceEN> GetBoatResourceFiltered(BoatResourceFilters filter);
    }
}
