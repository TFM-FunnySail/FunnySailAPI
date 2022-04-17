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
    public class OwnerInvoiceLineCEN : IOwnerInvoiceLineCEN
    {
        protected readonly IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        public OwnerInvoiceLineCEN(IOwnerInvoiceLineCAD ownerInvoiceLineCAD)
        {
            _ownerInvoiceLineCAD = ownerInvoiceLineCAD;
        }
        public async Task<IList<OwnerInvoiceLineEN>> GetAll(OwnerInvoiceLineFilters filters = null,
           Pagination pagination = null,
           Func<IQueryable<OwnerInvoiceLineEN>, IOrderedQueryable<OwnerInvoiceLineEN>> orderBy = null,
           Func<IQueryable<OwnerInvoiceLineEN>, IIncludableQueryable<OwnerInvoiceLineEN, object>> includeProperties = null)
        {
            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.BookingId);

            return await _ownerInvoiceLineCAD.Get(filters, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(OwnerInvoiceLineFilters filters)
        {
            var ownerInvoices = _ownerInvoiceLineCAD.GetOwnerInvoiceLineFiltered(filters);

            return await _ownerInvoiceLineCAD.GetCounter(ownerInvoices);
        }

        public async Task CreateOwnerInvoiceLines(IList<OwnerInvoiceLineEN> ownerInvoiceLines)
        {
            await _ownerInvoiceLineCAD.AddRange(ownerInvoiceLines);
        } 
    }
}
