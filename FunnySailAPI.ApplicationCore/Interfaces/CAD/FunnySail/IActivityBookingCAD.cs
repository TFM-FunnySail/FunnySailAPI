using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IActivityBookingCAD : IBaseCAD<ActivityBookingEN>
    {
        Task<ActivityBookingEN> FindByIds(int idActivity, int idBooking);

        IQueryable<ActivityBookingEN> GetActivityBookingFiltered(ActivityBookingFilters filters);
    }
}
