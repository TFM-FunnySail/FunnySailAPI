using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.TechnicalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class TechnicalServiceAssembler
    {
        public static TechnicalServiceOutputDTO Convert(TechnicalServiceEN technicalService)
        {
            TechnicalServiceOutputDTO technicalServiceBoatOutput = new TechnicalServiceOutputDTO
            {
                Id = technicalService.Id,
                Price = technicalService.Price,
                Description = technicalService.Description,
                Active = technicalService.Active
            };

            return technicalServiceBoatOutput;
        }
    }
}
