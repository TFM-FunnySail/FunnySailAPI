using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface ITechnicalServiceCEN
    {
        Task<int> AddTechnicalService(decimal price, string description);
        Task<TechnicalServiceEN> UpdateTechnicalService(UpdateTechnicalServiceDTO updateServiceInput);
        Task DeleteService(int id);
        Task<int> AddTechnicalServiceBoat(ScheduleTechnicalServiceDTO scheduleTechnicalService);
    }
}
