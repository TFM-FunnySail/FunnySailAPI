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
    public interface IOwnerInvoiceCEN
    {
        Task<OwnerInvoiceEN> CancelOwnerInvoice(int ownerInvoiceId);
        IOwnerInvoiceLineCAD GetOwnerInvoiceLineCAD();
        IOwnerInvoiceCAD GetOwnerInvoiceCAD();
        Task<int> CreateOwnerInvoice(string ownerId, decimal amount, bool toCollet);
        Task<IList<OwnerInvoiceEN>> GetAll(OwnerInvoiceFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<OwnerInvoiceEN>, IOrderedQueryable<OwnerInvoiceEN>> orderBy = null,
        Func<IQueryable<OwnerInvoiceEN>, IIncludableQueryable<OwnerInvoiceEN, object>> includeProperties = null);
        Task<int> GetTotal(OwnerInvoiceFilters filters = null);
        Task<OwnerInvoiceEN> PayOwnerInvoice(int id);
    }
}
