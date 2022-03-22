using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class TechnicalServiceBoatCAD : BaseCAD<TechnicalServiceBoatEN>, ITechnicalServiceBoatCAD
    {
        public TechnicalServiceBoatCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyServiceWithBoat(int technicalServiceId)
        {
            return await _dbContext.TechnicalServiceBoat.AnyAsync(x => x.TechnicalServiceId == technicalServiceId);
        }

        public async Task<bool> IsServiceBusy(int technicalServiceId, DateTime serviceDate)
        {
            return await _dbContext.TechnicalServiceBoat.AnyAsync(x => x.TechnicalServiceId == technicalServiceId
            && x.ServiceDate.Year == serviceDate.Year && x.ServiceDate.Month == serviceDate.Month && x.ServiceDate.Day == serviceDate.Day 
            && x.ServiceDate.Hour == serviceDate.Hour);
        }

        public async Task<IList<TechnicalServiceBoatEN>> Get(TechnicalServiceBoatFilters filters,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IOrderedQueryable<TechnicalServiceBoatEN>> orderBy = null,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IIncludableQueryable<TechnicalServiceBoatEN, object>> includeProperties = null,
                                            Pagination pagination = null)
        {
            IQueryable<TechnicalServiceBoatEN> technicalServiceBoats = Filter(filters);

            return await base.Get(technicalServiceBoats, orderBy, includeProperties, pagination);
        }

        private IQueryable<TechnicalServiceBoatEN> Filter(TechnicalServiceBoatFilters filters)
        {
            IQueryable<TechnicalServiceBoatEN> ownerInvoiceLines = GetIQueryable();

            if (ownerInvoiceLines == null) return ownerInvoiceLines;

            if (filters.IdList?.Count > 0)
                ownerInvoiceLines = ownerInvoiceLines.Where(x => filters.IdList.Contains(x.Id));

            return ownerInvoiceLines;
        }

        public async Task SetOwnerInvoice(List<TechnicalServiceBoatEN> technicalServiceBoats, int newOwnerInvoice)
        {
            technicalServiceBoats.ForEach(x => x.OwnerInvoiceId = newOwnerInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
