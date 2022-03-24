using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IRefundCAD: IBaseCAD<RefundEN>
    {
        IQueryable<RefundEN> GetRefundFiltered(RefundFilters filters);
    }
}
