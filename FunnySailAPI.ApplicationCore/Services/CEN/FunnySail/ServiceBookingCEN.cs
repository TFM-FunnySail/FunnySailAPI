using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
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

        public IServiceBookingCAD GetServiceBookingCAD()
        {
            return _serviceBookingCAD;
        }
    }
}
