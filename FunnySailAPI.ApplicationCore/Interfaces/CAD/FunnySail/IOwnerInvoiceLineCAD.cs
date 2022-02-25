using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IOwnerInvoiceLineCAD: IBaseCAD<OwnerInvoiceLineEN>
    {
        Task<IList<OwnerInvoiceLineEN>> Get(OwnerInvoiceLineFilters filters,
                                            Func<IQueryable<OwnerInvoiceLineEN>, IOrderedQueryable<OwnerInvoiceLineEN>> orderBy = null,
                                            string includeProperties = "",
                                            Pagination pagination = null);
        Task SetOwnerInvoice(List<OwnerInvoiceLineEN> ownerInvoiceLines, int newOwnerInvoice);
    }
}
