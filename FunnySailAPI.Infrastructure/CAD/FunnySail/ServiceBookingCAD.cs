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

        public async Task<bool> AnyServiceWithBooking(int serviceId)
        {
            return await _dbContext.ServiceBookings.AnyAsync(x => x.ServiceId == serviceId);
        }

        public async Task<ServiceBookingEN> FindByIds(int idService, int idBooking)
        {
            return await _dbContext.ServiceBookings.FindAsync(idService, idBooking);
        }
    }
}
