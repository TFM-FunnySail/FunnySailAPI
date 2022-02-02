using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatCAD : BaseCAD<BoatEN>, IBoatCAD
    {
        public BoatCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BoatEN> FindByIdAllData(int boatId)
        {
            return await _dbContext.Boats
                .Include(x => x.BoatInfo)
                .Include(x => x.BoatPrices)
                .Include(x => x.BoatType)
                .Include(x => x.BoatResources)
                .Include(x => x.RequiredBoatTitles)
                .FirstOrDefaultAsync(x=> x.Id == boatId);
        }
    }
}
