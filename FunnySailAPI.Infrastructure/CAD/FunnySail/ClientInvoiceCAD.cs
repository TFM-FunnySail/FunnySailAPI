using FunnySailAPI.ApplicationCore.Interfaces.CAD;
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
    public class ClientInvoiceCAD : BaseCAD<ClientInvoiceEN>, IClientInvoiceCAD
    {
        public ClientInvoiceCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ClientInvoiceEN> GetClientInvoiceFiltered(ClientInvoiceFilters ClientInvoiceFilters)
        {
            IQueryable<ClientInvoiceEN> ClientInvoices = GetIQueryable();

            if (ClientInvoiceFilters == null)
                return ClientInvoices;

            if (ClientInvoiceFilters.Id != 0)
                ClientInvoices = ClientInvoices.Where(x => x.Id == ClientInvoiceFilters.Id);

            if (ClientInvoiceFilters.ClientId != null)
                ClientInvoices = ClientInvoices.Where(x => x.ClientId == ClientInvoiceFilters.ClientId);

            if(ClientInvoiceFilters.ClientEmail != null)
            {
                ClientInvoices = ClientInvoices.Include(x => x.Client).ThenInclude(x => x.ApplicationUser)
                    .Where(x => x.Client.ApplicationUser.Email == ClientInvoiceFilters.ClientEmail);
            }

            if (ClientInvoiceFilters.IsCanceled != null)
                ClientInvoices = ClientInvoices.Where(x => x.Canceled == ClientInvoiceFilters.IsCanceled);

            if (ClientInvoiceFilters.IsPaid != null)
                ClientInvoices = ClientInvoices.Where(x => x.Paid == ClientInvoiceFilters.IsPaid);
            
            if (ClientInvoiceFilters.MinPrice != 0)
                ClientInvoices = ClientInvoices.Where(x => x.TotalAmount >= ClientInvoiceFilters.MinPrice);

            if (ClientInvoiceFilters.MaxPrice != 0)
                ClientInvoices = ClientInvoices.Where(x => x.TotalAmount < ClientInvoiceFilters.MaxPrice);
            
            if (ClientInvoiceFilters.CreatedDaysRange?.InitialDate != null)
                ClientInvoices = ClientInvoices.Where(x => x.CreatedDate >= ClientInvoiceFilters.CreatedDaysRange.InitialDate);

            if (ClientInvoiceFilters.CreatedDaysRange?.EndDate != null)
                ClientInvoices = ClientInvoices.Where(x => x.CreatedDate < ClientInvoiceFilters.CreatedDaysRange.EndDate);
            
            return ClientInvoices;
        }

        public async Task<List<ClientInvoiceEN>> GetClientInvoiceList(ClientInvoiceFilters ClientInvoiceFilters)
        {
            IQueryable<ClientInvoiceEN> ClientInvoices = GetClientInvoiceFiltered(ClientInvoiceFilters);

            return await ClientInvoices.ToListAsync();
        }

    }
}
