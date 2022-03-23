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
    public interface IRefundCEN
    {
        Task<int> CreateRefund(int bookingID, string desc, decimal amountToReturn);
        Task<IList<RefundEN>> GetAll(RefundFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<RefundEN>, IOrderedQueryable<RefundEN>> orderBy = null,
        Func<IQueryable<RefundEN>, IIncludableQueryable<RefundEN, object>> includeProperties = null);
        Task<int> GetTotal(RefundFilters filters = null);
    }
}
