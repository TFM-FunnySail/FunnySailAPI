using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ActivityBookingCAD : BaseCAD<ActivityBookingEN>, IActivityBookingCAD
    {
        public ActivityBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ActivityBookingEN> FindByIds(int idActivity, int idBooking)
        {
            return await _dbContext.ActivityBookings.FindAsync(idActivity, idBooking);
        }
    }
}
