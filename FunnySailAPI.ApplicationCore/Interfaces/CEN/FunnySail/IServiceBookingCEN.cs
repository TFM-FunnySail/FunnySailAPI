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
    public interface IServiceBookingCEN
    {
        Task<Tuple<int, int>> CreateServiceBooking(ServiceBookingEN serviceBookingEN);
        IServiceBookingCAD GetServiceBookingCAD();

        Task<IList<ServiceBookingEN>> GetAll(ServiceBookingFilters filters = null,
                     Pagination pagination = null,
                     Func<IQueryable<ServiceBookingEN>, IOrderedQueryable<ServiceBookingEN>> orderBy = null,
                     Func<IQueryable<ServiceBookingEN>, IIncludableQueryable<ServiceBookingEN, object>> includeProperties = null);
        Task<int> GetTotal(ServiceBookingFilters filters = null);
    }
}
