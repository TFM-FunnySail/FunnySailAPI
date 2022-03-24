using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class PortCAD : BaseCAD<PortEN>, IPortCAD
    {
        public PortCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PortEN> FindByIdAllData(int bookingId)
        {
            return await _dbContext.Ports
               .Include(x => x.Moorings)
               .FirstOrDefaultAsync(x => x.Id == bookingId);
        }

        public IQueryable<PortEN> GetPortFiltered(PortFilters filters)
        {
            IQueryable<PortEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.Id != 0)
                query = query.Where(x => x.Id == filters.Id);

            if (filters.Name != null)
                query = query.Where(x => x.Name == filters.Name);

            if (filters.Location != null)
                query = query.Where(x => x.Location == filters.Location);

            return query;
        }
    }
}
