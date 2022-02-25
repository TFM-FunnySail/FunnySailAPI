﻿using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IServiceBookingCEN
    {
        Task<Tuple<int, int>> CreateServiceBooking(ServiceBookingEN serviceBookingEN);
        IServiceBookingCAD GetServiceBookingCAD();
    }
}