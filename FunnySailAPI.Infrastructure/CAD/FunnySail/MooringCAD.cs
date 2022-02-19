using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class MooringCAD : BaseCAD<MooringEN>, IMooringCAD
    {
        public MooringCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AnyBoatWithMooring(int mooringId)
        {
            return _dbContext.Boats.AnyAsync(x => x.MooringId == mooringId);
        }

        public async Task<MooringEN> FindByIdAllData(int id,bool port = false)
        {
            IQueryable<MooringEN> moorings = GetIQueryable();

            if (port)
                moorings = moorings.Include(x => x.Port);

            return await moorings.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
