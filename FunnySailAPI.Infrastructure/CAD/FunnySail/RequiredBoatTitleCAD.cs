using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class RequiredBoatTitleCAD : BaseCAD<RequiredBoatTitleEN>, IRequiredBoatTitleCAD
    {
        public RequiredBoatTitleCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task AddOrRemove(IEnumerable<RequiredBoatTitleEN> requiredBoatTitles)
        {
            //Agrupando los titulos requeridos por barco
            var requireTitlesByBoat = requiredBoatTitles.GroupBy(x => new { x.BoatId },
                (f, g) => new {
                    BoatId = f.BoatId,
                    TitleIds = g.Select(x=>x.TitleId).ToList(),
                    requiredBoatTitles = g.ToList()
                });
            List<RequiredBoatTitleEN> boatTitlesToRemove = new List<RequiredBoatTitleEN>();
            List<RequiredBoatTitleEN> boatTitlesToAdd = new List<RequiredBoatTitleEN>();

            foreach(var item in requireTitlesByBoat)
            {
                List<RequiredBoatTitleEN> dbRequireBoatsTitles = await _dbContext.RequiredBoatTitles
                    .Where(x => x.BoatId ==  item.BoatId)
                    .ToListAsync();

                //Lista de titulos que estan en base de datos pero no vienen en la lista nueva
                var titlesToRemove = dbRequireBoatsTitles.Where(x =>!item.TitleIds.Contains(x.TitleId))
                    .ToList();

                //titulos en base de datos actualmente, listos para filtrar
                List<BoatTiteEnum> titlesIds = dbRequireBoatsTitles.Select(x => x.TitleId).ToList();

                var titlesToAdd = item.requiredBoatTitles.Where(x => !titlesIds.Contains(x.TitleId))
                    .ToList();

                boatTitlesToRemove.AddRange(titlesToRemove);
                boatTitlesToAdd.AddRange(titlesToAdd);
            }

            await _dbContext.RequiredBoatTitles.AddRangeAsync(boatTitlesToAdd);
            _dbContext.RequiredBoatTitles.RemoveRange(boatTitlesToRemove);

            await _dbContext.SaveChangesAsync();
        }
    }
}
