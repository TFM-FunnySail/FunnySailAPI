using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class UsersCAD : BaseCAD<UsersEN>, IUserCAD
    {
        public UsersCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UsersEN> GetFiltered(UsersFilters filters)
        {
            IQueryable<UsersEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.UserId != null)
                query = query.Where(x => x.UserId == filters.UserId);

            if (filters.Email != null)
                query = query.Include(x=>x.ApplicationUser)
                    .Where(x => x.ApplicationUser.Email == filters.Email);

            if (filters.ReceivePromotion != null)
                query = query.Where(x => x.ReceivePromotion == filters.ReceivePromotion);

            if (filters.BoatOwner != null)
                query = query.Where(x => x.BoatOwner == filters.BoatOwner);

            if (filters.BirthDay?.InitialDate != null)
                query = query.Where(x => x.BirthDay >= filters.BirthDay.InitialDate);

            if (filters.BirthDay?.EndDate != null)
                query = query.Where(x => x.BirthDay < filters.BirthDay.EndDate);

            if (filters.FirstName != null)
                query = query.Where(x => x.FirstName.Contains(filters.FirstName));

            if (filters.LastName != null)
                query = query.Where(x => x.LastName.Contains(filters.LastName));

            return query;
        }
    }
}
