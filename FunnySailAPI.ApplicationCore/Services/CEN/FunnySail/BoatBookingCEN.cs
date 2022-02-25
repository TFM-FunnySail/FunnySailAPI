using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
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

        public IBoatBookingCAD GetBoatBookingCAD() 
        {
            return _boatBookingCAD;
        }
    }
}
