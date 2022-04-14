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
    public class BookingCAD : BaseCAD<BookingEN>, IBookingCAD
    {
        public BookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BookingEN> FindByIdAllData(int bookingId)
        {
            return await _dbContext.Bookings
                .Include(x => x.InvoiceLine)
                .Include(x => x.OwnerInvoiceLines)
                .Include(x => x.ServiceBookings)
                .Include(x => x.ActivityBookings)
                .Include(x => x.BoatBookings)
                .FirstOrDefaultAsync(x => x.Id == bookingId);
        }

        public IQueryable<BookingEN> GetBookingFiltered(BookingFilters filters)
        {
            IQueryable<BookingEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.bookingId != 0)
                query = query.Where(x => x.Id == filters.bookingId);

            if (filters.ClientId != null)
                query = query.Where(x => x.ClientId == filters.ClientId);

            if (filters.CreatedDateRange != null)
            {
                if(filters.CreatedDateRange.InitialDate != null)
                    query = query.Where(x => x.CreatedDate >= filters.CreatedDateRange.InitialDate);

                if (filters.CreatedDateRange.EndDate != null)
                    query = query.Where(x => x.CreatedDate < filters.CreatedDateRange.EndDate);
            }

            if (filters.EntryDateRange != null)
            {
                if (filters.EntryDateRange.InitialDate != null)
                    query = query.Where(x => x.EntryDate >= filters.EntryDateRange.InitialDate);

                if (filters.EntryDateRange.EndDate != null)
                    query = query.Where(x => x.EntryDate < filters.EntryDateRange.EndDate);
            }

            if (filters.DepartureDateRange != null)
            {
                if (filters.DepartureDateRange.InitialDate != null)
                    query = query.Where(x => x.DepartureDate >= filters.DepartureDateRange.InitialDate);

                if (filters.DepartureDateRange.EndDate != null)
                    query = query.Where(x => x.DepartureDate < filters.DepartureDateRange.EndDate);
            }

            if (filters.TotalPeople != 0)
                query = query.Where(x => x.TotalPeople == filters.TotalPeople);

            if (filters.Paid != null)
                query = query.Where(x => x.Paid == filters.Paid);

            if (filters.RequestCaptain != null)
                query = query.Where(x => x.RequestCaptain == filters.RequestCaptain);

            return query;
        }
    }
}
