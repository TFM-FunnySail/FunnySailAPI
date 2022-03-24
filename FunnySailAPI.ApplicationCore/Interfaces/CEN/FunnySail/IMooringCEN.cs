using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IMooringCEN
    {
        Task<int> AddMooring(int portId, string alias, MooringEnum type);
        Task DeleteMooring(int mooringId);
        Task<MooringEN> UpdateMooring(UpdateMooringDTO updateMooringInput);
        Task<bool> Any(MooringFilters filter);
        Task<int> GetTodos(MooringFilters filters = null);
        Task<IList<MooringEN>> GetAll(MooringFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<MooringEN>, IOrderedQueryable<MooringEN>> orderBy = null,
            Func<IQueryable<MooringEN>, IIncludableQueryable<MooringEN, object>> includeProperties = null);

    }
}
