using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ServiceCEN : IServiceCEN
    {
        private readonly IServiceCAD _serviceCAD;
        public ServiceCEN(IServiceCAD serviceCAD)
        {
            _serviceCAD = serviceCAD;
        }

        public async Task<int> AddService(string name,decimal price,string description)
        {
            if(name == null)
                throw new DataValidationException("Service name", "Nombre del servicio",
                    ExceptionTypesEnum.IsRequired);

            if (description == null)
                throw new DataValidationException("Service description", "Descripción del servicio",
                    ExceptionTypesEnum.IsRequired);

            ServiceEN service = await _serviceCAD.AddAsync(new ServiceEN
            {
                Description = description,
                Name = name, 
                Price = price
            });

            return service.Id;
        } 
    }
}
