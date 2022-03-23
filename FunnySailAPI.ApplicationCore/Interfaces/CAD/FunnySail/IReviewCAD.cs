using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IReviewCAD : IBaseCAD<ReviewEN>
    {
        IQueryable<ReviewEN> GetReviewFiltered(ReviewFilters filters);
    }
}
