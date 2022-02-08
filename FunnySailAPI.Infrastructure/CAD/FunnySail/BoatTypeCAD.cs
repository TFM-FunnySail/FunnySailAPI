using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
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
    }
}
