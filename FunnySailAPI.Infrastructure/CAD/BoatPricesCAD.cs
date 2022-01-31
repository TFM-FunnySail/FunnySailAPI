using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class BoatPricesCAD : BaseCAD<BoatPricesEN>, IBoatPricesCAD
    {
        public BoatPricesCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
