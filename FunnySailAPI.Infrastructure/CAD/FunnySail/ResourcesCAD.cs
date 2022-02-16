using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ResourcesCAD : BaseCAD<ResourcesEN>, IResourcesCAD
    {
        public ResourcesCAD(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
