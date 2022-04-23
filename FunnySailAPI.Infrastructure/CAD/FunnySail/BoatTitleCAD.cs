using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
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
    public class BoatTitleCAD : BaseCAD<BoatTitlesEN>, IBoatTitleCAD
    {
        public BoatTitleCAD(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> AnyBoatWithTitle(int titleId)
        {
            return _dbContext.RequiredBoatTitles.AnyAsync(x => x.TitleId == titleId);
        }

        public async Task<BoatTitlesEN> FindByIdAllData(int id, bool requiredTitle = false)
        {
            IQueryable<BoatTitlesEN> titles = GetIQueryable();

            if (requiredTitle)
                titles = titles.Include(x => x.RequiredBoatTitles);

            return await titles.FirstOrDefaultAsync(x => x.TitleId == id);
        }

        #region Filter
        public IQueryable<BoatTitlesEN> GetRequiredTitleFiltered(BoatTitlesFilters requiredTitlesFilters)
        {
            IQueryable<BoatTitlesEN> requiredTitles = GetIQueryable();

            if (requiredTitlesFilters == null)
                return requiredTitles;

            if (requiredTitlesFilters.TitleId != 0)
                requiredTitles = requiredTitles.Where(x => x.TitleId == requiredTitlesFilters.TitleId);

            if (requiredTitlesFilters.Name != null)
                requiredTitles = requiredTitles.Where(x => x.Name.Contains(requiredTitlesFilters.Name));

            if (requiredTitlesFilters.Description != null)
                requiredTitles = requiredTitles.Where(x => x.Description.Contains(requiredTitlesFilters.Description));

            return requiredTitles;
        }

        #endregion
    }
}
