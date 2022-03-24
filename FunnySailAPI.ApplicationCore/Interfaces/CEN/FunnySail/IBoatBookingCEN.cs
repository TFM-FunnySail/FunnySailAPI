using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
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
    public interface IBoatBookingCEN
    {
        Task<Tuple<int, int>> CreateBoatBooking(BoatBookingEN boatBookingEN);
        IBoatBookingCAD GetBoatBookingCAD();

        Task<IList<BoatBookingEN>> GetAll(BoatBookingFilters filters = null,
                            Pagination pagination = null,
                            Func<IQueryable<BoatBookingEN>, IOrderedQueryable<BoatBookingEN>> orderBy = null,
                            Func<IQueryable<BoatBookingEN>, IIncludableQueryable<BoatBookingEN, object>> includeProperties = null);
        Task<int> GetTotal(BoatBookingFilters filters = null);
    }
}
