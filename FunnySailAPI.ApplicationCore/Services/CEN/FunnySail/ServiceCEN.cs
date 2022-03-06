using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
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
        private readonly IServiceBookingCAD _serviceBookingCAD;
        public ServiceCEN(IServiceCAD serviceCAD,
                          IServiceBookingCAD serviceBookingCAD)
        {
            _serviceCAD = serviceCAD;
            _serviceBookingCAD = serviceBookingCAD;
        }

        public async Task<int> CreateService(string name,decimal price,string description)
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

        public async Task<ServiceEN> UpdateService(UpdateServiceDTO updateServiceInput)
        {
            ServiceEN service = await _serviceCAD.FindById(updateServiceInput.Id);

            if (service == null)
                throw new DataValidationException("Service", "Servicio",
                    ExceptionTypesEnum.NotFound);

            if (updateServiceInput.Name == null)
                throw new DataValidationException("Service name", "Nombre del servicio",
                    ExceptionTypesEnum.IsRequired);

            if (updateServiceInput.Description == null)
                throw new DataValidationException("Service description", "Descripción del servicio",
                    ExceptionTypesEnum.IsRequired);

            service.Name = updateServiceInput.Name;
            service.Price = updateServiceInput.Price;
            service.Description = updateServiceInput.Description;
            service.Active = updateServiceInput.Active;

            await _serviceCAD.Update(service);

            return service;
        }

        public async Task DeleteService(int id)
        {
            ServiceEN service = await _serviceCAD.FindById(id);

            if (service == null)
                throw new DataValidationException("Service", "Servicio",
                    ExceptionTypesEnum.NotFound);

            bool serviceWithBooking = await _serviceBookingCAD.AnyServiceWithBooking(id);

            if (serviceWithBooking)
            {
                service.Active = false;
                await _serviceCAD.Update(service);
            }
            else
            {
                await _serviceCAD.Delete(service);
            }
        }

        public IServiceCAD GetServiceCAD()
        {
            return _serviceCAD;
        }
    }
}
