﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Port;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public class PortAssemblers
    {
        public static PortOutputDTO Convert(PortEN portEN)
        {
            PortOutputDTO portOutputDTO = new PortOutputDTO
            {
                Id = portEN.Id,
                Location = portEN.Location,
                Name = portEN.Name,
            };

            if (portEN.Moorings != null)
            {
                foreach (var mooring in portEN.Moorings)
                {
                    //portOutputDTO.Moorings.Add(MooringAssembler.Convert(mooring));
                }
            }
            return portOutputDTO;
        }
    }
}
