using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
 
    public class ActivityCAD : BaseCAD<ActivityEN>, IActivityCAD
    {
        public ActivityCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
