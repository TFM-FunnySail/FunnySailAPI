using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class OrderRatesCAD : BaseCAD<OrderRatesEN>, IOrderRatesCAD
    {
        public OrderRatesCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
