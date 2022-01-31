using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class RequiredBoatTitleCAD : BaseCAD<RequiredBoatTitleEN>, IRequiredBoatTitleCAD
    {
        public RequiredBoatTitleCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
