using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatResourceCEN : IBoatResourceCEN
    {
        private readonly IBoatResourceCAD _boatResourceCAD;

        public BoatResourceCEN(IBoatResourceCAD boatResourceCAD)
        {
            _boatResourceCAD = boatResourceCAD;
        }

        public async Task<(int,int)> AddBoatResource(BoatResourceEN boatResourceEN)
        {
            if (boatResourceEN.BoatId == 0)
                throw new DataValidationException("Boat id", "Id del barco", ExceptionTypesEnum.IsRequired);

            boatResourceEN = await _boatResourceCAD.AddAsync(boatResourceEN);

            return (boatResourceEN.BoatId, boatResourceEN.ResourceId);
        }

    }
}
