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
    public interface IOwnerInvoiceLineCEN
    {
        Task<IList<OwnerInvoiceLineEN>> GetAll(OwnerInvoiceLineFilters filters = null,
           Pagination pagination = null,
           Func<IQueryable<OwnerInvoiceLineEN>, IOrderedQueryable<OwnerInvoiceLineEN>> orderBy = null,
           Func<IQueryable<OwnerInvoiceLineEN>, IIncludableQueryable<OwnerInvoiceLineEN, object>> includeProperties = null);
        Task<int> GetTotal(OwnerInvoiceLineFilters filters);
        Task CreateOwnerInvoiceLines(IList<OwnerInvoiceLineEN> ownerInvoiceLines);
    }
}
