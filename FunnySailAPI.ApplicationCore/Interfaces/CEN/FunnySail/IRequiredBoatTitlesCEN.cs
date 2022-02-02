using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IRequiredBoatTitlesCEN
    {
        Task<(int, BoatTiteEnum)> AddRequiredBoatTitle(RequiredBoatTitleEN requiredBoatTitleEN);
    }
}
