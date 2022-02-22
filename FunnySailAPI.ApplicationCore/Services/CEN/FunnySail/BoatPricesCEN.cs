using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatPricesCEN : BoatBaseCEN,IBoatPricesCEN
    {
        private readonly IBoatPricesCAD _boatPricesCAD;

        public BoatPricesCEN(IBoatPricesCAD boatPricesCAD)
        {
            _boatPricesCAD = boatPricesCAD;
        }

        public async Task<int> AddBoatPrices(BoatPricesEN boatPricesEN)
        {
            boatPricesEN = await _boatPricesCAD.AddAsync(boatPricesEN);

            return boatPricesEN.BoatId;
        }

        public async Task<BoatPricesEN> UpdateBoat(UpdateBoatPricesInputDTO updateBoatInput)
        {
            BoatPricesEN boatPrices = await _boatPricesCAD.FindById(updateBoatInput.BoatId);

            if (boatPrices == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);

            boatPrices.DayBasePrice = updateBoatInput.DayBasePrice;
            boatPrices.HourBasePrice = updateBoatInput.HourBasePrice;
            boatPrices.Supplement = updateBoatInput.Supplement;

            await _boatPricesCAD.Update(boatPrices);

            return boatPrices;
        }
    }
}
