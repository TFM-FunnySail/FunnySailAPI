using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class ServiceAssemblers
    {
        public static ServiceOutputDTO Convert(ServiceEN service)
        {
            ServiceOutputDTO serviceOutput = new ServiceOutputDTO
            {
                Id = service.Id, 
                Active = service.Active,
                Description = service.Description,
                Name = service.Name,
                Price = service.Price
            };

            return serviceOutput;
        }
    }
}
