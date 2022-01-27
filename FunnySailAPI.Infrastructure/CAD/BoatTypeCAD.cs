using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class BoatTypeCAD : BaseCAD<BoatTypeEN>, IBoatTypeCAD
    {
        private ApplicationDbContext _dbContext;
        public BoatTypeCAD(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddBoatType(string name, string description)
        {
            BoatTypeEN boatType = await AddAsync(new BoatTypeEN
            {
                Name = name,
                Description = description
            });

            return boatType.Id;
        } 
    }
}
