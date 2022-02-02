using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BookingCAD : BaseCAD<BookingEN>, IBookingCAD
    {
        public BookingCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
