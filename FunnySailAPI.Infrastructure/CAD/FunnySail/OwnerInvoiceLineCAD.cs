using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
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

        private IQueryable<OwnerInvoiceLineEN> GetOwnerInvoiceLineFiltered(OwnerInvoiceLineFilters filters)
        {
            IQueryable<OwnerInvoiceLineEN> ownerInvoiceLines = GetIQueryable();

            if (ownerInvoiceLines == null) return ownerInvoiceLines;

            if (filters.BookingIds?.Count > 0)
                ownerInvoiceLines = ownerInvoiceLines.Where(x => filters.BookingIds.Contains(x.BookingId));

            return ownerInvoiceLines;
        }

        public async Task<IList<OwnerInvoiceLineEN>> Get(OwnerInvoiceLineFilters filters,
                                            Func<IQueryable<OwnerInvoiceLineEN>, IOrderedQueryable<OwnerInvoiceLineEN>> orderBy = null,
                                            string includeProperties = "",
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
    }
}
