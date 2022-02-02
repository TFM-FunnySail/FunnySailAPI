using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatTypeCEN : IBoatTypeCEN
    {
        private readonly IBoatTypeCAD _boatTypeCAD;

        public BoatTypeCEN(IBoatTypeCAD boatTypeCAD)
        {
            _boatTypeCAD = boatTypeCAD;
        }

        public Task<bool> AnyBoatTypeById(int boatTypeId)
        {
            IQueryable<BoatTypeEN> boatTypesQueyrable = _boatTypeCAD.GetIQueryable();
            boatTypesQueyrable = boatTypesQueyrable.Where(x => x.Id == boatTypeId);

            return _boatTypeCAD.Any(boatTypesQueyrable);
        }
    }
}
