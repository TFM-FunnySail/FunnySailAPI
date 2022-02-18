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

        public async Task<MooringEN> FindByIdAllData(int id,bool boat = false,bool port = false)
        {
            IQueryable<MooringEN> moorings = GetIQueryable();

            if (boat)
                moorings = moorings.Include(x => x.Boat);

            if (port)
                moorings = moorings.Include(x => x.Port);

            return await moorings.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
