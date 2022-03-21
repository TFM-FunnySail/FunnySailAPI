using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IOwnerInvoiceCAD : IBaseCAD<OwnerInvoiceEN>
    {
        IQueryable<OwnerInvoiceEN> GetOwnerInvoiceFiltered(OwnerInvoiceFilters OwnerInvoiceFilters);
        Task<List<OwnerInvoiceEN>> GetOwnerInvoiceList(OwnerInvoiceFilters OwnerInvoiceFilters);
   
    }
}
