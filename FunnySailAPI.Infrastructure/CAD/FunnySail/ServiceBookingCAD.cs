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
    public class ServiceBookingCAD : BaseCAD<ServiceBookingEN>, IServiceBookingCAD
    {
        public ServiceBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyServiceWithBooking(int serviceId)
        {
            return await _dbContext.ServiceBookings.AnyAsync(x => x.ServiceId == serviceId);
        }

        public async Task<ServiceBookingEN> FindByIds(int idService, int idBooking)
        {
            return await _dbContext.ServiceBookings.FindAsync(idService, idBooking);
        }

        public IQueryable<ServiceBookingEN> GetServiceBookingFiltered(ServiceBookingFilters filters)
        {
            IQueryable<ServiceBookingEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.ServiceId != 0)
                query = query.Where(x => x.ServiceId == filters.ServiceId);

            if (filters.RangePrice != (null, null))
                query = query.Where(x => filters.RangePrice.Item1 <= x.Price && x.Price <= filters.RangePrice.Item2);


            return query;
        }
    }
}
