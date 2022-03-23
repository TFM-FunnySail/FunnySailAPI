using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatResourceCAD : BaseCAD<BoatResourceEN>, IBoatResourceCAD
    {
        public BoatResourceCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(int,int)> AddBoatResource(int boatId,int resourceId)
        {
            BoatResourceEN boatResource = await AddAsync(new BoatResourceEN
            {
                ResourceId = resourceId,
                BoatId = boatId
            });

            return (boatResource.BoatId,boatResource.ResourceId);
        }

        public IQueryable<BoatResourceEN> GetBoatResourceFiltered(BoatResourceFilters filter)
        {
            IQueryable<BoatResourceEN> query = GetIQueryable();

            if (filter == null)
                return query;

            if (filter.BoatId != 0)
                query = query.Where(x => x.BoatId == filter.BoatId);

            if (filter.ResourceId != 0)
                query = query.Where(x => x.ResourceId == filter.ResourceId);

            if (filter.NotResourceId != null)
                query = query.Where(x => filter.NotResourceId.Contains(x.ResourceId));

            if (filter.NotBoatId != null)
                query = query.Where(x => !filter.NotBoatId.Contains(x.BoatId));

            return query;
        }
    }
}
