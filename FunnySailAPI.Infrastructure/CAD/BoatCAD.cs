using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class BoatCAD : BaseCAD<BoatEN>, IBoatCAD
    {
        public BoatCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
