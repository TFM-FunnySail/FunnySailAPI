using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Mooring;
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
                //Como se tiene el id de puerto, el atributo Port daba problemas y lo he quitado, si no ejecuta, pro
                Id = mooringEN.Id,
                PortId = mooringEN.PortId,
                Type = mooringEN.Type,
                Alias = mooringEN.Alias
            };

            return mooringOutput;
        }
    }
}
