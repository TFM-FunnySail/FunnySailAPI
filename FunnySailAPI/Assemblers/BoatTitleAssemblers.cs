using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Boat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class BoatTitleAssemblers
    {
        public static BoatTitleOutputDTO Convert(BoatTitlesEN boatTitleEN)
        {
            BoatTitleOutputDTO boatTitleOutput = new BoatTitleOutputDTO 
            {
                TitleId = boatTitleEN.TitleId,
                Description = boatTitleEN.Description,
                Name = boatTitleEN.Name,

            };


            return boatTitleOutput;
        }
    }
}
