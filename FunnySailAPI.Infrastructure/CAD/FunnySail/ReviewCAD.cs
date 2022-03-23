using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class ReviewCAD : BaseCAD<ReviewEN>, IReviewCAD
    {
        public ReviewCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ReviewEN> GetReviewFiltered(ReviewFilters filters)
        {
            IQueryable<ReviewEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.Id != 0)
                query = query.Where(x => x.Id == filters.Id);

            if (filters.AdminId != null)
                query = query.Where(x => x.AdminId == filters.AdminId);

            if (filters.BoatId != 0)
                query = query.Where(x => x.BoatId >= filters.BoatId);

            return query;
        }
    }
}
