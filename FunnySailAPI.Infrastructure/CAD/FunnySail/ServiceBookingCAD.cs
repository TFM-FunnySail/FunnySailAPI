using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ServiceBookingCAD : BaseCAD<ServiceBookingEN>, IServiceBookingCAD
    {
        public ServiceBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AnyServiceWithBooking(int serviceId)
        {
            return _dbContext.ServiceBookings.AnyAsync(x => x.ServiceId == serviceId);
        }
    }
}
