using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatBookingCAD : BaseCAD<BoatBookingEN>, IBoatBookingCAD
    {
        public BoatBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
