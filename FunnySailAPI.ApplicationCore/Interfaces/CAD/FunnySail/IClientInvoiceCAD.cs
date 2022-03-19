using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IClientInvoiceCAD : IBaseCAD<ClientInvoiceEN>
    {
        IQueryable<ClientInvoiceEN> GetClientInvoiceFiltered(ClientInvoiceFilters ClientInvoiceFilters);
        Task<List<ClientInvoiceEN>> GetClientInvoiceList(ClientInvoiceFilters ClientInvoiceFilters);
    }
}
