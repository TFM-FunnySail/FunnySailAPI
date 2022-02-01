using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class OrderCAD : BaseCAD<OrderEN>, IOrderCAD
    {
        public OrderCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
