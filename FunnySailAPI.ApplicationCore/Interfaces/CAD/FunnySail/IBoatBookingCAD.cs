using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBoatBookingCAD : IBaseCAD<BoatBookingEN>
    {
        Task<BoatBookingEN> FindByIds(int idBoat, int idBooking);
    }
}
