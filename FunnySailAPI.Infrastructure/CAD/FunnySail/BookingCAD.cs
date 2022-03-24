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

            if (filters.CreatedDate != null)
                query = query.Where(x => x.CreatedDate == filters.CreatedDate);

            if (filters.EntryDate != null)
                query = query.Where(x => x.EntryDate == filters.EntryDate);

            if (filters.DepartureDate != null)
                query = query.Where(x => x.DepartureDate == filters.DepartureDate);
            
            if (filters.TotalPeople != null)
                query = query.Where(x => x.TotalPeople == filters.TotalPeople);

            if (filters.Paid != null)
                query = query.Where(x => x.Paid == filters.Paid);

            if (filters.RequestCaptain != null)
                query = query.Where(x => x.RequestCaptain == filters.RequestCaptain);

            return query;
        }
    }
}
