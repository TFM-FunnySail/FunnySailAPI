using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface ITechnicalServiceBoatCAD : IBaseCAD<TechnicalServiceBoatEN>
    {
        Task<bool> AnyServiceWithBoat(int id);
        Task<bool> IsServiceBusy(int technicalServiceId, DateTime serviceDate);
    }
}
