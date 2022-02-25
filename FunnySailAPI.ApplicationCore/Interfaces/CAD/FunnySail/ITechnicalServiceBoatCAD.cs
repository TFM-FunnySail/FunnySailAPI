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
    public interface ITechnicalServiceBoatCAD : IBaseCAD<TechnicalServiceBoatEN>
    {
        Task<bool> AnyServiceWithBoat(int id);
        Task<bool> IsServiceBusy(int technicalServiceId, DateTime serviceDate);
        Task<IList<TechnicalServiceBoatEN>> Get(TechnicalServiceBoatFilters filters,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IOrderedQueryable<TechnicalServiceBoatEN>> orderBy = null,
                                            string includeProperties = "",
                                            Pagination pagination = null);
        Task SetOwnerInvoice(List<TechnicalServiceBoatEN> technicalServiceBoats, int newOwnerInvoice);
    }
}
