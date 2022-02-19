using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN
{
    public interface IServiceCEN
    {
        Task<int> AddService(string name, decimal price, string description);
        Task<ServiceEN> UpdateService(UpdateServiceDTO updateServiceInput);
    }
}
