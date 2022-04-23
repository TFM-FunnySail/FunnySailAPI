using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatTypeCAD : BaseCAD<BoatTypeEN>, IBoatTypeCAD
    {
        public BoatTypeCAD(ApplicationDbContext dbContext) :base(dbContext)
        {
        }

        public async Task<int> AddBoatType(string name, string description)
        {
            if (name == null || description == null)
                throw new ArgumentException("The name or the description is null");

            BoatTypeEN boatType = await AddAsync(new BoatTypeEN
            {
                Name = name,
                Description = description
            });

            return boatType.Id;
        }

        #region Filter
        public IQueryable<BoatTypeEN> GetBoatTypesFiltered(BoatTypesFilters boatTypesFilters)
        {
            IQueryable<BoatTypeEN> boatTypes = GetIQueryable();

            if (boatTypesFilters == null)
                return boatTypes;

            if (boatTypesFilters.Id != 0)
                boatTypes = boatTypes.Where(x => x.Id == boatTypesFilters.Id);

            if (boatTypesFilters.Name != null)
                boatTypes = boatTypes.Where(x => x.Name.Contains(boatTypesFilters.Name));

            if (boatTypesFilters.Description != null)
                boatTypes = boatTypes.Where(x => x.Description.Contains(boatTypesFilters.Description));

            return boatTypes;
        }

        #endregion
    }
}
