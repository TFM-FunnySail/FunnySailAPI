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
    public class TechnicalServiceCEN : ITechnicalServiceCEN
    {
        private readonly ITechnicalServiceCAD _technicalServiceCAD;
        private readonly ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        public TechnicalServiceCEN(ITechnicalServiceCAD technicalServiceCAD,
                          ITechnicalServiceBoatCAD technicalServiceBoatCAD)
        {
            _technicalServiceCAD = technicalServiceCAD;
            _technicalServiceBoatCAD = technicalServiceBoatCAD;
        }

        public async Task<int> AddTechnicalService(decimal price, string description)
        {
            if (description == null)
                throw new DataValidationException("Technical service description", "Descripción del servicio técnico",
                    ExceptionTypesEnum.IsRequired);

            TechnicalServiceEN service = await _technicalServiceCAD.AddAsync(new TechnicalServiceEN
            {
                Description = description,
                Price = price
            });

            return service.Id;
        }
    }
}
