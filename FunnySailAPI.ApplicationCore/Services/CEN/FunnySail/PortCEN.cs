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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;

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

        public async Task<PortEN> EditPort(UpdatePortDTO updatePortInput)
        {

            PortEN port = await _portCAD.FindById(updatePortInput.Id);

            if (port == null)
                throw new DataValidationException("Port", "Puerto",
                    ExceptionTypesEnum.NotFound);

            port.Name = updatePortInput.Name;
            port.Location = updatePortInput.Location;

            await _portCAD.Update(port);

            return port;
        }

        public async Task DeletePort(int id)
        {
            PortEN port = await _portCAD.FindById(id);

            if (port == null)
                throw new DataValidationException("Port", "Puerto",
                    ExceptionTypesEnum.NotFound);

                await _portCAD.Delete(port);  
        }

        public IPortCAD GetPortCAD()
        {
            return _portCAD;
        }

        public Task<bool> AnyPortById(int portId)
        {
            IQueryable<PortEN> port = _portCAD.GetIQueryable();
            return _portCAD.Any(port.Where(x => x.Id == portId));
        }
    }
}
