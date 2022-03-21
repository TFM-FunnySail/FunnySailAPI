using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IClientInvoiceLineCAD : IBaseCAD<InvoiceLineEN>
    {
        Task<InvoiceLineEN> FindByIds(int idBooking, int? idClientInvoice);
        Task<IList<InvoiceLineEN>> Get(ClientsInvoiceLineFilters filters,
                                       Func<IQueryable<InvoiceLineEN>, IOrderedQueryable<InvoiceLineEN>> orderBy = null,
                                       Func<IQueryable<InvoiceLineEN>, IIncludableQueryable<InvoiceLineEN, object>> includeProperties = null,
                                       Pagination pagination = null);
        Task SetOwnerInvoice(List<InvoiceLineEN> clientInvoiceLines, int newClientInvoice);
    }
}
