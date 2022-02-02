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

        public async Task<string> AddBoatResource(int boatId,string uri, bool main, BoatResourcesEnum type)
        {
            BoatResourceEN boatResource = await AddAsync(new BoatResourceEN
            {
                Uri = uri,
                Main = main,
                Type = type,
                BoatId = boatId
            });

            return boatResource.Uri;
        }
    }
}
