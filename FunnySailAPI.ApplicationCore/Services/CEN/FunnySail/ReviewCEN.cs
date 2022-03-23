using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ReviewCEN : IReviewCEN
    {
        private readonly IReviewCAD _reviewCAD;

        public ReviewCEN(IReviewCAD reviewCAD)
        {
            _reviewCAD = reviewCAD;
        }

        public async Task<int> AddReview(int boatId, string adminId, string observation)
        {
            ReviewEN dbReview = await _reviewCAD.AddAsync(new ReviewEN
            {
                AdminId = adminId,
                BoatId = boatId,
                Description = observation
            });

            return dbReview.Id;
        }

        public async Task<IList<ReviewEN>> GetAll(ReviewFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ReviewEN>, IOrderedQueryable<ReviewEN>> orderBy = null,
        Func<IQueryable<ReviewEN>, IIncludableQueryable<ReviewEN, object>> includeProperties = null)
        {
            var query = _reviewCAD.GetReviewFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _reviewCAD.Get(query, orderBy, includeProperties, pagination);
        }
        public async Task<int> GetTotal(ReviewFilters filters = null)
        {
            var query = _reviewCAD.GetReviewFiltered(filters);

            return await _reviewCAD.GetCounter(query);
        }
    }
}
