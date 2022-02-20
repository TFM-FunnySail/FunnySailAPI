using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class TechnicalServiceBoatCAD : BaseCAD<TechnicalServiceBoatEN>, ITechnicalServiceBoatCAD
    {
        public TechnicalServiceBoatCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyServiceWithBoat(int technicalServiceId)
        {
            return await _dbContext.TechnicalServiceBoat.AnyAsync(x => x.TechnicalServiceId == technicalServiceId);
        }
    }
}
