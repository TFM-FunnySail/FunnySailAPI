using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
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
    public interface IClientInvoiceCEN
    {
        Task<ClientInvoiceEN> CancelClientInvoice(int clientInvoiceId);
        Task<int> CreateClientInvoice(ClientInvoiceEN clientInvoiceEN);
        IClientInvoiceCAD GetClientInvoiceCAD();
        Task<int> GetTotal(ClientInvoiceFilters filters = null);
        Task<IList<ClientInvoiceEN>> GetAll(ClientInvoiceFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ClientInvoiceEN>, IOrderedQueryable<ClientInvoiceEN>> orderBy = null,
        Func<IQueryable<ClientInvoiceEN>, IIncludableQueryable<ClientInvoiceEN, object>> includeProperties = null);
    }
}
