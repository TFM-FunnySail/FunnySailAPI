using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatBookingCAD : BaseCAD<BoatBookingEN>, IBoatBookingCAD
    {
        public BoatBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BoatBookingEN> FindByIds(int idBoat, int idBooking)
        {
            return await _dbContext.BoatBookings.FindAsync(idBoat, idBooking);
        }

        public IQueryable<BoatBookingEN> GetBoatBookingFiltered(BoatBookingFilters filters)
        {
            IQueryable<BoatBookingEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.BoatId != 0)
                query = query.Where(x => x.BoatId == filters.BoatId);

            if (filters.RangePrice != (null, null))
                query = query.Where(x => filters.RangePrice.Item1 <= x.Price && x.Price <= filters.RangePrice.Item2);


            return query;
        }
    }
}
