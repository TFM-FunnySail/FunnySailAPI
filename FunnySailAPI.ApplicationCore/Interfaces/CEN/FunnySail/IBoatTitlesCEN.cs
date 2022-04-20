using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatTitlesCEN
    {
        Task<int> AddBoatTitle(AddBoatTitleInputDTO addBoatTitleInput);
        Task DeleteBoatTitle(int titleId);
        Task<BoatTitlesEN> UpdateBoatTitle(UpdateBoatTitleDTO updateBoatTitleInput);
        IBoatTitleCAD GetBoatTitleCAD();
        Task<IList<BoatTitlesEN>> GetAll(BoatTitlesFilters filters = null,
                  Pagination pagination = null,
                  Func<IQueryable<BoatTitlesEN>, IOrderedQueryable<BoatTitlesEN>> orderBy = null,
                  Func<IQueryable<BoatTitlesEN>, IIncludableQueryable<BoatTitlesEN, object>> includeProperties = null);

        Task<int> GetTotal(BoatTitlesFilters filters = null);
    }
}
