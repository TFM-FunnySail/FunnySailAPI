using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ServiceBookingCEN : IServiceBookingCEN
    {
        private readonly IServiceBookingCAD _serviceBookingCAD;

        public ServiceBookingCEN(IServiceBookingCAD serviceBookingCAD)
        {
            _serviceBookingCAD = serviceBookingCAD;
        }

        public async Task<Tuple<int, int>> CreateServiceBooking(ServiceBookingEN serviceBookingEN)
        {
            serviceBookingEN = await _serviceBookingCAD.AddAsync(serviceBookingEN);

            return new Tuple<int,int>(serviceBookingEN.ServiceId, serviceBookingEN.BookingId);
        }

        public async Task<IList<ServiceBookingEN>> GetAll(ServiceBookingFilters filters = null, Pagination pagination = null, Func<IQueryable<ServiceBookingEN>, IOrderedQueryable<ServiceBookingEN>> orderBy = null, Func<IQueryable<ServiceBookingEN>, IIncludableQueryable<ServiceBookingEN, object>> includeProperties = null)
        {
            var query = _serviceBookingCAD.GetServiceBookingFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.ServiceId);

            return await _serviceBookingCAD.Get(query, orderBy, includeProperties, pagination);
        }

        public IServiceBookingCAD GetServiceBookingCAD()
        {
            return _serviceBookingCAD;
        }

        public async Task<int> GetTotal(ServiceBookingFilters filters = null)
        {
            var query = _serviceBookingCAD.GetServiceBookingFiltered(filters);

            return await _serviceBookingCAD.GetCounter(query);
        }
    }
}
