using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class TechnicalServiceCAD : BaseCAD<TechnicalServiceEN>, ITechnicalServiceCAD
    {
        public TechnicalServiceCAD(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
