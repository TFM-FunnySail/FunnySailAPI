using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatPricesCEN : IBoatPricesCEN
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
    }
}
