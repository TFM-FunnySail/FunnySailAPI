using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class OwnerInvoiceCAD : BaseCAD<OwnerInvoiceEN>, IOwnerInvoiceCAD
    {
        public OwnerInvoiceCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<OwnerInvoiceEN> GetOwnerInvoiceFiltered(OwnerInvoiceFilters OwnerInvoiceFilters)
        {
            IQueryable<OwnerInvoiceEN> OwnerInvoices = GetIQueryable();

            if (OwnerInvoiceFilters == null)
                return OwnerInvoices;

            if (OwnerInvoiceFilters.Id != 0)
                OwnerInvoices = OwnerInvoices.Where(x => x.Id == OwnerInvoiceFilters.Id);

            if (OwnerInvoiceFilters.OwnerId != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.OwnerId == OwnerInvoiceFilters.OwnerId);

            if (OwnerInvoiceFilters.IsCanceled != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.IsCanceled == OwnerInvoiceFilters.IsCanceled);

            if (OwnerInvoiceFilters.IsPaid != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.IsPaid == OwnerInvoiceFilters.IsPaid);

            if (OwnerInvoiceFilters.ToCollet != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.ToCollet == OwnerInvoiceFilters.ToCollet);

            if (OwnerInvoiceFilters.MinPrice != 0)
                OwnerInvoices = OwnerInvoices.Where(x => x.Amount >= OwnerInvoiceFilters.MinPrice);

            if (OwnerInvoiceFilters.MaxPrice != 0)
                OwnerInvoices = OwnerInvoices.Where(x => x.Amount < OwnerInvoiceFilters.MaxPrice);

            if (OwnerInvoiceFilters.CreatedDaysRange?.InitialDate != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.Date >= OwnerInvoiceFilters.CreatedDaysRange.InitialDate);

            if (OwnerInvoiceFilters.CreatedDaysRange?.EndDate != null)
                OwnerInvoices = OwnerInvoices.Where(x => x.Date < OwnerInvoiceFilters.CreatedDaysRange.EndDate);

            if (OwnerInvoiceFilters.OwnerEmail != null)
                OwnerInvoices = OwnerInvoices.Include(x => x.Owner)
                    .ThenInclude(x => x.ApplicationUser)
                    .Where(x => x.Owner.ApplicationUser.Email == OwnerInvoiceFilters.OwnerEmail);

            return OwnerInvoices;
        }

        public async Task<List<OwnerInvoiceEN>> GetOwnerInvoiceList(OwnerInvoiceFilters OwnerInvoiceFilters)
        {
            IQueryable<OwnerInvoiceEN> OwnerInvoices = GetOwnerInvoiceFiltered(OwnerInvoiceFilters);

            return await OwnerInvoices.ToListAsync();
        }

    }
}
