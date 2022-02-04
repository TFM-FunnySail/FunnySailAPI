using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ReviewCAD : BaseCAD<ReviewEN>, IReviewCAD
    {
        public ReviewCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
