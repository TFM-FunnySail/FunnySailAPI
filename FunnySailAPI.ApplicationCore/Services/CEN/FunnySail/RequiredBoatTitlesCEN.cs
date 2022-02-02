using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class RequiredBoatTitlesCEN : IRequiredBoatTitlesCEN
    {
        private readonly IRequiredBoatTitleCAD _requiredBoatTitleCAD;

        public RequiredBoatTitlesCEN(IRequiredBoatTitleCAD requiredBoatTitleCAD)
        {
            _requiredBoatTitleCAD = requiredBoatTitleCAD;
        }

        public async Task<(int, BoatTiteEnum)> AddRequiredBoatTitle(RequiredBoatTitleEN requiredBoatTitleEN)
        {
            requiredBoatTitleEN = await _requiredBoatTitleCAD.AddAsync(requiredBoatTitleEN);

            return (requiredBoatTitleEN.BoatId, requiredBoatTitleEN.TitleId);
        }
    }
}
