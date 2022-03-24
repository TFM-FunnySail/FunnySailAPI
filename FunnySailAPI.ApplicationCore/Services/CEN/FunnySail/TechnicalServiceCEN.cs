using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class TechnicalServiceCEN : ITechnicalServiceCEN
    {
        private readonly ITechnicalServiceCAD _technicalServiceCAD;
        private readonly ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        private readonly string _enName;
        private readonly string _esName;
        public TechnicalServiceCEN(ITechnicalServiceCAD technicalServiceCAD,
                          ITechnicalServiceBoatCAD technicalServiceBoatCAD)
        {
            _technicalServiceCAD = technicalServiceCAD;
            _technicalServiceBoatCAD = technicalServiceBoatCAD;
            _enName = "Technical service";
            _esName = "Servicio técnico";
        }

        public async Task<int> AddTechnicalService(decimal price, string description)
        {
            if (description == null)
                throw new DataValidationException($"{_enName} description", $"Descripción del {_esName}",
                    ExceptionTypesEnum.IsRequired);

            TechnicalServiceEN service = await _technicalServiceCAD.AddAsync(new TechnicalServiceEN
            {
                Description = description,
                Price = price
            });

            return service.Id;
        }

        public async Task<TechnicalServiceEN> UpdateTechnicalService(UpdateTechnicalServiceDTO updateServiceInput)
        {
            TechnicalServiceEN service = await _technicalServiceCAD.FindById(updateServiceInput.Id);

            if (service == null)
                throw new DataValidationException(_enName, _esName,
                    ExceptionTypesEnum.NotFound);

            service.Price = updateServiceInput.Price;
            service.Description = updateServiceInput.Description;
            service.Active = updateServiceInput.Active;

            await _technicalServiceCAD.Update(service);

            return service;
        }

        public async Task DeleteService(int id)
        {
            TechnicalServiceEN service = await _technicalServiceCAD.FindById(id);

            if (service == null)
                throw new DataValidationException(_enName, _esName,
                    ExceptionTypesEnum.NotFound);

            bool serviceWithBoat = await _technicalServiceBoatCAD.AnyServiceWithBoat(id);

            if (serviceWithBoat)
            {
                service.Active = false;
                await _technicalServiceCAD.Update(service);
            }
            else
            {
                await _technicalServiceCAD.Delete(service);
            }
        }

        public async Task<int> AddTechnicalServiceBoat(ScheduleTechnicalServiceDTO scheduleTechnicalService)
        {
            TechnicalServiceEN service = await _technicalServiceCAD.
                FindById(scheduleTechnicalService.TechnicalServiceId);

            if (service == null)
                throw new DataValidationException(_enName, _esName,
                    ExceptionTypesEnum.NotFound);

            bool technicalServiceBusy = await _technicalServiceBoatCAD.IsServiceBusy(scheduleTechnicalService.TechnicalServiceId,
                scheduleTechnicalService.ServiceDate);
            if (technicalServiceBusy)
                throw new DataValidationException("Technical support is not available for the requested date",
                    "El servicio técnico no está disponible para la fecha solicitada");

            TechnicalServiceBoatEN technicalServiceBoat = await _technicalServiceBoatCAD.AddAsync(new TechnicalServiceBoatEN
            {
                Done = false,
                CreatedDate = DateTime.UtcNow,
                BoatId = scheduleTechnicalService.BoatId,
                Price = scheduleTechnicalService.Price,
                ServiceDate = scheduleTechnicalService.ServiceDate,
                TechnicalServiceId = service.Id
            });

            return technicalServiceBoat.Id;
        }

        public async Task<IList<TechnicalServiceEN>> GetAll(TechnicalServiceFilters filters = null,
       Pagination pagination = null,
       Func<IQueryable<TechnicalServiceEN>, IOrderedQueryable<TechnicalServiceEN>> orderBy = null,
       Func<IQueryable<TechnicalServiceEN>, IIncludableQueryable<TechnicalServiceEN, object>> includeProperties = null)
        {
            var technicalServices = _technicalServiceCAD.GetTechnicalServiceFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _technicalServiceCAD.Get(technicalServices, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(TechnicalServiceFilters filters = null)
        {
            var TechnicalServices = _technicalServiceCAD.GetTechnicalServiceFiltered(filters);

            return await _technicalServiceCAD.GetCounter(TechnicalServices);
        }
    }
}
