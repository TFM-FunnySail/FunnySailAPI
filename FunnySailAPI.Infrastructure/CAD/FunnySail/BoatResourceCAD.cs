using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
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
    }
}
