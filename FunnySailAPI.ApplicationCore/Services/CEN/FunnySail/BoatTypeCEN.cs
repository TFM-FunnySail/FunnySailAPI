using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
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

        public async Task<IList<BoatTypeEN>> GetAll(BoatTypesFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatTypeEN>, IOrderedQueryable<BoatTypeEN>> orderBy = null,
            Func<IQueryable<BoatTypeEN>, IIncludableQueryable<BoatTypeEN, object>> includeProperties = null)
        {
            var boatTypes = _boatTypeCAD.GetBoatTypesFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _boatTypeCAD.Get(boatTypes, orderBy, includeProperties, pagination);
        }

    }
}
