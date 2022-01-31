using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class UsersCAD : BaseCAD<UsersEN>, IUserCAD
    {
        public UsersCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
