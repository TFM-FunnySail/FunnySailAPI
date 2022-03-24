using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Mooring;
using FunnySailAPI.DTO.Output.Port;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class MooringAssemblers
    {
        public static MooringOutputDTO Convert(MooringEN mooringEN)
        {
            MooringOutputDTO mooringOutput = new MooringOutputDTO
            {
                Id = mooringEN.Id,
                PortId = mooringEN.PortId,
                Type = mooringEN.Type,
                Alias = mooringEN.Alias
            };

            if(mooringEN.Port != null)
            {
                mooringOutput.Port = new PortOutputDTO
                {
                    Id = mooringEN.Port.Id,
                    Location = mooringEN.Port.Location,
                    Name = mooringEN.Port.Name,
                };
            }


            return mooringOutput;
        }
    }
}
