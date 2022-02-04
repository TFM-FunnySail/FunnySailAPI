using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ServiceBookingCAD : BaseCAD<ServiceBookingEN>, IServiceBookingCAD
    {
        public ServiceBookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
