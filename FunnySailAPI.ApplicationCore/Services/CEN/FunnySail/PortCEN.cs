using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class PortCEN : IPortCEN
    {
        private readonly IPortCAD _portCAD;

        public PortCEN(IPortCAD portCAD)
        {
            _portCAD = portCAD;
        }

        public async Task<int> AddPort(string name, string location)
        {
            PortEN dbPort = await _portCAD.AddAsync(new PortEN
            {
                Name = name,
                Location = location
            });

            return dbPort.Id;
        }

        public IPortCAD GetPortCAD()
        {
            return _portCAD;
        }

    }
}
