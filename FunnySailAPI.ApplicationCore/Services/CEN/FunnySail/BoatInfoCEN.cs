using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatInfoCEN : BoatBaseCEN, IBoatInfoCEN
    {
        private readonly IBoatInfoCAD _boatInfoCAD;

        public BoatInfoCEN(IBoatInfoCAD boatInfoCAD)
        {
            _boatInfoCAD = boatInfoCAD;
        }

        public async Task<int> AddBoatInfo(BoatInfoEN boatInfoEN)
        {
            if (boatInfoEN.Name == null)
                throw new DataValidationException("Boat name ", "Barco nombre ", ExceptionTypesEnum.IsRequired);

            if (boatInfoEN.Description == null)
                throw new DataValidationException("Boat description ", " la descripcion Barco ", ExceptionTypesEnum.IsRequired);

            if (boatInfoEN.Registration == null)
                throw new DataValidationException("Boat Registration ", " el registro de Barco ", ExceptionTypesEnum.IsRequired);

            if (boatInfoEN.MooringPoint == null)
                throw new DataValidationException("Boat MooringPoint ", " el punto de amarre de Barco ", ExceptionTypesEnum.IsRequired);

            boatInfoEN = await _boatInfoCAD.AddAsync(boatInfoEN);

            return boatInfoEN.BoatId;
        }

        public async Task<BoatInfoEN> UpdateBoat(UpdateBoatInfoInputDTO updateBoatInput)
        {
            BoatInfoEN boatInfo = await _boatInfoCAD.FindById(updateBoatInput.BoatId);

            if (boatInfo == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);

            boatInfo.Capacity = updateBoatInput.Capacity;
            boatInfo.Description = updateBoatInput.Description;
            boatInfo.Length = updateBoatInput.Length;
            boatInfo.MotorPower = updateBoatInput.MotorPower;
            boatInfo.Name = updateBoatInput.Name;
            boatInfo.Registration = updateBoatInput.Registration;
            boatInfo.Sleeve = updateBoatInput.Sleeve;

            await _boatInfoCAD.Update(boatInfo);

            return boatInfo;
        }
    }
}
