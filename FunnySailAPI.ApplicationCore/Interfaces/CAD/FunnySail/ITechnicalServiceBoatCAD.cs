using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface ITechnicalServiceBoatCAD : IBaseCAD<TechnicalServiceBoatEN>
    {
        Task<bool> AnyServiceWithBoat(int id);
        Task<bool> IsServiceBusy(int technicalServiceId, DateTime serviceDate);
        Task<IList<TechnicalServiceBoatEN>> Get(TechnicalServiceBoatFilters filters,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IOrderedQueryable<TechnicalServiceBoatEN>> orderBy = null,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IIncludableQueryable<TechnicalServiceBoatEN, object>> includeProperties = null,
                                            Pagination pagination = null);
        Task SetOwnerInvoice(List<TechnicalServiceBoatEN> technicalServiceBoats, int newOwnerInvoice);
    }
}
