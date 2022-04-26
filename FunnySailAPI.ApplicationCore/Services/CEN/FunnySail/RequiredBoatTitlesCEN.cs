using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class RequiredBoatTitlesCEN : BoatBaseCEN,IRequiredBoatTitlesCEN
    {
        private readonly IRequiredBoatTitleCAD _requiredBoatTitleCAD;

        public RequiredBoatTitlesCEN(IRequiredBoatTitleCAD requiredBoatTitleCAD)
        {
            _requiredBoatTitleCAD = requiredBoatTitleCAD;
        }

        public async Task<(int, int)> AddRequiredBoatTitle(RequiredBoatTitleEN requiredBoatTitleEN)
        {
            if (requiredBoatTitleEN.BoatId == 0)
                throw new DataValidationException("Boat id", "Id del barco", ExceptionTypesEnum.IsRequired);

            requiredBoatTitleEN = await _requiredBoatTitleCAD.AddAsync(requiredBoatTitleEN);

            return (requiredBoatTitleEN.BoatId, requiredBoatTitleEN.TitleId);
        }

        public async Task<List<RequiredBoatTitleEN>> UpdateRequiredBoatTitle(UpdateRequiredBoatTitleDTO requiredBoatTitle)
        {
            List<RequiredBoatTitleEN> requiredBoatTitleEn = requiredBoatTitle.BoatTites.Select(x => new RequiredBoatTitleEN
            {
                BoatId = requiredBoatTitle.BoatId,
                TitleId = x
            }).ToList();
            await _requiredBoatTitleCAD.AddOrRemove(requiredBoatTitleEn);

            return requiredBoatTitleEn;
        }
    }
}
