using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatBookingCEN : IBoatBookingCEN
    {
        private readonly IBoatBookingCAD _boatBookingCAD;

        public BoatBookingCEN(IBoatBookingCAD boatBookingCAD)
        {
            _boatBookingCAD = boatBookingCAD;
        }

        public async Task<Tuple<int, int>> CreateBoatBooking(BoatBookingEN boatBookingEN)
        {
            boatBookingEN = await _boatBookingCAD.AddAsync(boatBookingEN);

            return new Tuple<int, int>(boatBookingEN.BoatId, boatBookingEN.BookingId);
        }

        public Task<IList<BoatBookingEN>> GetAll(BoatBookingFilters filters = null, Pagination pagination = null, Func<IQueryable<BoatBookingEN>, IOrderedQueryable<BoatBookingEN>> orderBy = null, Func<IQueryable<BoatBookingEN>, IIncludableQueryable<BoatBookingEN, object>> includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IBoatBookingCAD GetBoatBookingCAD() 
        {
            return _boatBookingCAD;
        }

        public Task<int> GetTotal(BoatBookingFilters filters = null)
        {
            throw new NotImplementedException();
        }
    }
}
