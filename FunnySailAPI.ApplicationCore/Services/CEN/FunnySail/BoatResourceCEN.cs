using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<BoatResourceEN>> GetAll(BoatResourceFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatResourceEN>, IOrderedQueryable<BoatResourceEN>> orderBy = null,
            Func<IQueryable<BoatResourceEN>, IIncludableQueryable<BoatResourceEN, object>> includeProperties = null)
        {
            var boats = _boatResourceCAD.GetBoatResourceFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.BoatId).ThenBy(x=>x.ResourceId);

            return await _boatResourceCAD.Get(boats, orderBy, includeProperties, pagination);
        }

        public IBoatResourceCAD GetBoatResourceCAD()
        {
            return _boatResourceCAD;
        }
    }
}
