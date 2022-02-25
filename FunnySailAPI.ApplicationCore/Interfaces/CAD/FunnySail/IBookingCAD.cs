﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBookingCAD : IBaseCAD<BookingEN>
    {
        Task<BookingEN> FindByIdAllData(int bookingId);
    }
}
