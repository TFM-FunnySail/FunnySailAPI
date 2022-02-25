using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Include(x => x.ClientId)
                .Include(x => x.CreatedDate)
                .Include(x => x.EntryDate)
                .Include(x => x.DepartureDate)
                .Include(x => x.TotalPeople)
                .Include(x => x.Paid)
                .Include(x => x.RequestCaptain)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == bookingId);
        }
    }
}
