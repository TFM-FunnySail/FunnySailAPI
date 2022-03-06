using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatCEN : BoatBaseCEN, IBoatCEN
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

        public async Task<BoatEN> ApproveBoat(int boatId)
        {
            BoatEN dbBoat = await _boatCAD.FindById(boatId);

            if (dbBoat == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);

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

        public async Task<IList<BoatEN>> GetAvailableBoats(Pagination pagination, DateTime initialDate, DateTime endDate,
            Func<IQueryable<BoatEN>, IOrderedQueryable<BoatEN>> orderBy = null,
            Func<IQueryable<BoatEN>, IIncludableQueryable<BoatEN, object>> includeProperties = null)
        {
            List<int> idsNotAvailable = await _boatCAD.GetBoatIdsNotAvailable(initialDate, endDate);

            IQueryable<BoatEN> boats = _boatCAD.GetBoatFiltered(new BoatFilters
            {
                Active = true,
                ExclusiveBoatId = idsNotAvailable
            });

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _boatCAD.Get(boats, orderBy, includeProperties, pagination);
        }

        public async Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput)
        {
            BoatEN boat = await _boatCAD.FindById(updateBoatInput.BoatId);

            if (boat == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);

            boat.MooringId = updateBoatInput.MooringId;
            boat.BoatTypeId = updateBoatInput.BoatTypeId;

            await _boatCAD.Update(boat);

            return boat;
        }

        public async Task<IList<BoatEN>> GetAll(BoatFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatEN>, IOrderedQueryable<BoatEN>> orderBy = null,
            Func<IQueryable<BoatEN>, IIncludableQueryable<BoatEN, object>> includeProperties = null)
        {
            var boats = _boatCAD.GetBoatFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _boatCAD.Get(boats, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(BoatFilters filters = null)
        {
            var boats = _boatCAD.GetBoatFiltered(filters);

            return await _boatCAD.GetCounter(boats);
        }
    }
}
