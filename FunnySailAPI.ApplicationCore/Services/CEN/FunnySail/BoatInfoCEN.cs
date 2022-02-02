using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatInfoCEN : IBoatInfoCEN
    {
        private readonly IBoatInfoCAD _boatInfoCAD;

        public BoatInfoCEN(IBoatInfoCAD boatInfoCAD)
        {
            _boatInfoCAD = boatInfoCAD;
        }

        public async Task<int> AddBoatInfo(BoatInfoEN boatInfoEN)
        {
            boatInfoEN = await _boatInfoCAD.AddAsync(boatInfoEN);

            return boatInfoEN.BoatId;
        }
    }
}
