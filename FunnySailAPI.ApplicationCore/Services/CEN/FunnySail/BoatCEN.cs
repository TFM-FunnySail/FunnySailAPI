using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatCEN : IBoatCEN
    {
        private readonly IBoatCAD _boatCAD;

        public BoatCEN(IBoatCAD boatCAD)
        {
            _boatCAD = boatCAD;
        }

        public async Task<int> CreateBoat(BoatEN boatEN)
        {
            boatEN = await _boatCAD.AddAsync(boatEN);

            return boatEN.Id;
        }

        public async Task<BoatEN> GetAllDataBoat(int boatId)
        {
            return await _boatCAD.FindByIdAllData(boatId);
        }

        public async Task<BoatEN> ApproveBoat(int boatId)
        {
            BoatEN dbBoat = await _boatCAD.FindById(boatId);

            if (dbBoat == null)
                throw new DataValidationException("Boat not found.",
                    "La embarcación no se encuentra.");

            dbBoat.PendingToReview = false;
            dbBoat.Active = true;

            await _boatCAD.Update(dbBoat);

            return dbBoat;
        }

        public async Task<BoatEN> DisapproveBoat(int boatId)
        {
            BoatEN boat = await _boatCAD.FindById(boatId);

            boat.PendingToReview = false;
            boat.Active = false;

            await _boatCAD.Update(boat);

            return boat;
        }

        public IBoatCAD GetBoatCAD()
        {
            return _boatCAD;
        }
    }
}
