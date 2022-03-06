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
    public class ServiceCAD : BaseCAD<ServiceEN>, IServiceCAD
    {
        public ServiceCAD(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        }

        public async Task<ServiceEN> FindByIdAllData(int serviceId)
        {
            return await _dbContext.Services
            .Include(x => x.Name)
            .Include(x => x.Price)
            .Include(x => x.ServiceBookings)
            .FirstOrDefaultAsync(x => x.Id == serviceId);
        }

 
        public async Task<List<int>> GetServiceIdsNotAvailable(DateTime initialDate, DateTime endDate)
        {
            return await _dbContext.Bookings.
                Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
                (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
                .Join(_dbContext.ServiceBookings,
                s => s.Id, sb => sb.BookingId,
                (booking, serviceBooking) => serviceBooking.ServiceId)
                .Distinct().ToListAsync();
        }

        public async Task<List<int>> GetServiceIdsNotAvailable(DateTime initialDate, DateTime endDate, List<int> ids)
        {
            return await _dbContext.Bookings.
               Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
               (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
               .Join(_dbContext.ServiceBookings.Where(x => ids.Contains(x.ServiceId)),
               s => s.Id, sb => sb.BookingId,
               (booking, serviceBooking) => serviceBooking.ServiceId)
               .Distinct().ToListAsync();
        }

        #region Filter
        public IQueryable<ServiceEN> GetServiceFiltered(ServiceFilters serviceFilters)
        {
            IQueryable<ServiceEN> services = GetIQueryable();

            if(serviceFilters == null)
                return services;

            if (serviceFilters.ServiceId != 0)
                services = services.Where(x => x.Id == serviceFilters.ServiceId);
            
            if (serviceFilters.Active != null)
                services = services.Where(x => x.Active == serviceFilters.Active);

            if (serviceFilters.ServiceIdList?.Count > 0)
                services = services.Where(x => serviceFilters.ServiceIdList.Contains(x.Id));

            return services;
        }

        public async Task<List<ServiceEN>> GetServiceFilteredList(ServiceFilters serviceFilters)
        {
            IQueryable<ServiceEN> boats = GetServiceFiltered(serviceFilters);

            return await boats.ToListAsync();
        }
        #endregion
    }

}
