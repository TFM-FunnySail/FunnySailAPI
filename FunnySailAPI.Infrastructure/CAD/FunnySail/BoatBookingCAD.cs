using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
