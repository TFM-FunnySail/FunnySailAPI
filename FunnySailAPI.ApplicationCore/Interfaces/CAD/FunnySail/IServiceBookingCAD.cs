﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IServiceBookingCAD : IBaseCAD<ServiceBookingEN>
    {
        Task<bool> AnyServiceWithBooking(int id);
        Task<ServiceBookingEN> FindByIds(int idService, int idBooking);
    }
}
