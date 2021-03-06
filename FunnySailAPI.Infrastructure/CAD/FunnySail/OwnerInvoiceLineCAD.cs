using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class OwnerInvoiceLineCAD : BaseCAD<OwnerInvoiceLineEN>,IOwnerInvoiceLineCAD
    {
        public OwnerInvoiceLineCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public IQueryable<OwnerInvoiceLineEN> GetOwnerInvoiceLineFiltered(OwnerInvoiceLineFilters filters)
        {
            IQueryable<OwnerInvoiceLineEN> ownerInvoiceLines = GetIQueryable();

            if (filters == null) return ownerInvoiceLines;

            if (filters.BookingIds?.Count > 0)
                ownerInvoiceLines = ownerInvoiceLines.Where(x => filters.BookingIds.Contains(x.BookingId));

            if (filters.OwnerId != null)
                ownerInvoiceLines = ownerInvoiceLines.Where(x => x.OwnerId == filters.OwnerId);

            if (filters.OwnerInvoiceId != 0)
                ownerInvoiceLines = ownerInvoiceLines.Where(x => x.OwnerInvoiceId == filters.OwnerInvoiceId);

            if(filters.Invoiced != null)
            {
                if(filters.Invoiced == true)
                    ownerInvoiceLines = ownerInvoiceLines.Where(x => x.OwnerInvoiceId != null);
                else
                    ownerInvoiceLines = ownerInvoiceLines.Where(x => x.OwnerInvoiceId == null);
            }

            return ownerInvoiceLines;
        }

        public async Task<IList<OwnerInvoiceLineEN>> Get(OwnerInvoiceLineFilters filters,
                                            Func<IQueryable<OwnerInvoiceLineEN>, IOrderedQueryable<OwnerInvoiceLineEN>> orderBy = null,
                                            Func<IQueryable<OwnerInvoiceLineEN>, IIncludableQueryable<OwnerInvoiceLineEN, object>> includeProperties = null,
                                            Pagination pagination = null)
        {
            IQueryable<OwnerInvoiceLineEN> ownerInvoiceLines = GetOwnerInvoiceLineFiltered(filters);

            return await Get(ownerInvoiceLines,orderBy,includeProperties,pagination);
        }

        public async Task SetOwnerInvoice(List<OwnerInvoiceLineEN> ownerInvoiceLines, int newOwnerInvoice)
        {
            ownerInvoiceLines.ForEach(x => x.OwnerInvoiceId = newOwnerInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<OwnerInvoiceLineEN> ownerInvoices)
        {
            await _dbContext.OwnerInvoiceLines.AddRangeAsync(ownerInvoices);
            await _dbContext.SaveChangesAsync();
        }
    }
}
