using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityBookingCEN : IActivityBookingCEN
    {
        private readonly IActivityBookingCAD _activityBookingCAD;

        public ActivityBookingCEN(IActivityBookingCAD activityBookingCAD)
        {
            _activityBookingCAD = activityBookingCAD;
        }

        public async Task<Tuple<int, int>> CreateActivityBooking(ActivityBookingEN activityBookingEN)
        {
            activityBookingEN = await _activityBookingCAD.AddAsync(activityBookingEN);

            return new Tuple<int, int>(activityBookingEN.ActivityId, activityBookingEN.BookingId);
        }

        public IActivityBookingCAD GetActivityBookingCAD()
        {
            return _activityBookingCAD;
        }
    }
}
