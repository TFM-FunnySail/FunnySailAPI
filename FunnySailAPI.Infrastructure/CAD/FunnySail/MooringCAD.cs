using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class MooringCAD : BaseCAD<MooringEN>, IMooringCAD
    {
        public MooringCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
