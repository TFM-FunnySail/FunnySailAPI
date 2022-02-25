using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
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
            && x.ServiceDate == serviceDate);
        }

        public async Task<IList<TechnicalServiceBoatEN>> Get(TechnicalServiceBoatFilters filters,
                                            Func<IQueryable<TechnicalServiceBoatEN>, IOrderedQueryable<TechnicalServiceBoatEN>> orderBy = null,
                                            string includeProperties = "",
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
