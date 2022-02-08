using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new DataValidationException("Boat", "La embarcación", ExceptionTypesEnum.NotFound);

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

        public async Task<List<BoatEN>> GetAvailableBoats(Pagination pagination, DateTime initialDate, DateTime endDate)
        {
            List<int> idsNotAvailable = await _boatCAD.GetBoatIdsNotAvailable(initialDate, endDate);

            IQueryable<BoatEN> boats = _boatCAD.GetBoatFiltered(new BoatFilters
            {
                Active = true,
                CreatedDaysRange = new DaysRangeFilter
                {
                    EndDate = endDate,
                    InitialDate = initialDate
                },
                ExclusiveBoatId = idsNotAvailable
            });

            return await _boatCAD.GetAll(boats.OrderBy(x => x.Id), pagination);
        }
    }
}
