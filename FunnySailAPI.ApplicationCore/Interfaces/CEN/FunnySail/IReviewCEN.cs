using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IReviewCEN
    {
        Task<int> AddReview(int boatId, string adminId, string observation);
        Task<IList<ReviewEN>> GetAll(ReviewFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ReviewEN>, IOrderedQueryable<ReviewEN>> orderBy = null,
        Func<IQueryable<ReviewEN>, IIncludableQueryable<ReviewEN, object>> includeProperties = null);
        Task<int> GetTotal(ReviewFilters filters = null);
        Task<ReviewEN> CloseReview(int id);
    }
}
