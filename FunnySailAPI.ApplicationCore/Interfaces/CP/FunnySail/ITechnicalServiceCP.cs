using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface ITechnicalServiceCP
    {
        Task<int> ScheduleTechnicalServiceToBoat(ScheduleTechnicalServiceDTO scheduleTechnicalService);
    }
}
