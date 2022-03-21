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
    public class ClientInvoiceLineCAD : BaseCAD<InvoiceLineEN>, IClientInvoiceLineCAD
    {
        public ClientInvoiceLineCAD(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<InvoiceLineEN> FindByIds(int idBooking, int? idClientInvoice)
        {
            if (idClientInvoice != null)
            {
                return await _dbContext.InvoiceLines.FindAsync(idBooking, idClientInvoice);
            }
            else
            {
                return await _dbContext.InvoiceLines.FindAsync(idBooking);
            }
        }

        private IQueryable<InvoiceLineEN> GetClientInvoiceLineFiltered(ClientsInvoiceLineFilters filters)
        {
            IQueryable<InvoiceLineEN> clientInvoiceLines = GetIQueryable();

            if (clientInvoiceLines == null) return clientInvoiceLines;

            if (filters.BookingIds?.Count > 0)
                clientInvoiceLines = clientInvoiceLines.Where(x => filters.BookingIds.Contains(x.BookingId));

            return clientInvoiceLines;
        }

        public async Task<IList<InvoiceLineEN>> Get(ClientsInvoiceLineFilters filters,
                                            Func<IQueryable<InvoiceLineEN>, IOrderedQueryable<InvoiceLineEN>> orderBy = null,
                                            Func<IQueryable<InvoiceLineEN>, IIncludableQueryable<InvoiceLineEN, object>> includeProperties = null,
                                            Pagination pagination = null)
        {
            IQueryable<InvoiceLineEN> clientInvoiceLines = GetClientInvoiceLineFiltered(filters);

            return await Get(clientInvoiceLines, orderBy, includeProperties, pagination);
        }

        public async Task SetOwnerInvoice(List<InvoiceLineEN> clientInvoiceLines, int newClientInvoice)
        {
            clientInvoiceLines.ForEach(x => x.ClientInvoiceId = newClientInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
