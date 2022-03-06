using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class TechnicalServiceCP : ITechnicalServiceCP
    {
        private readonly ITechnicalServiceCEN _technicalServiceCEN;
        private readonly IBoatCEN _boatCEN;

        public TechnicalServiceCP(ITechnicalServiceCEN technicalServiceCEN,
                                  IBoatCEN boatCEN)
        {
            _technicalServiceCEN = technicalServiceCEN;
            _boatCEN = boatCEN;
        }

        public async Task<int> ScheduleTechnicalServiceToBoat(ScheduleTechnicalServiceDTO scheduleTechnicalService)
        {
            if(await _boatCEN.GetBoatCAD().AnyById(scheduleTechnicalService.BoatId))
                throw new DataValidationException("Boat", "Embarcación", ExceptionTypesEnum.NotFound);

            bool boatBusy = await _boatCEN.GetBoatCAD().IsBoatBusy(scheduleTechnicalService.BoatId,
                scheduleTechnicalService.ServiceDate);

            if (boatBusy)
                throw new DataValidationException("The boat is not available for the requested date",
                    "La embarcación no está disponible para la fecha solicitada");

            return await _technicalServiceCEN.AddTechnicalServiceBoat(scheduleTechnicalService);
        }
    }
}
