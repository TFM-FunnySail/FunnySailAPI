using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Port;
using Microsoft.EntityFrameworkCore.Query;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class PortCEN : IPortCEN
    {
        private readonly IPortCAD _portCAD;
        private readonly string _enName;
        private readonly string _esName;

        public PortCEN(IPortCAD portCAD)
        {
            _portCAD = portCAD;
            _enName = "Port";
            _esName = "Puerto";
        }

        public async Task<int> AddPort(string name, string location)
        {
            if (name == null)
            {
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);
            }
            else if (location == null)
            {
                throw new DataValidationException($"{_enName} location", $"Localización del {_esName}",
                   ExceptionTypesEnum.IsRequired);
            }

            PortEN dbPort = await _portCAD.AddAsync(new PortEN
            {
                Name = name,
                Location = location
            });

            return dbPort.Id;
        }

        public async Task<PortEN> EditPort(UpdatePortDTO updatePortInput)
        {
            if (updatePortInput.Name == null)
            {
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);
            }
            else if (updatePortInput.Location == null)
            {
                throw new DataValidationException($"{_enName} location", $"Localización del {_esName}",
                   ExceptionTypesEnum.IsRequired);
            }

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

        public async Task<IList<PortEN>> GetAll(PortFilters filters = null, Pagination pagination = null, Func<IQueryable<PortEN>, IOrderedQueryable<PortEN>> orderBy = null, Func<IQueryable<PortEN>, IIncludableQueryable<PortEN, object>> includeProperties = null)
        {
            var query = _portCAD.GetPortFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _portCAD.Get(query, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(PortFilters filters = null)
        {
            var query = _portCAD.GetPortFiltered(filters);

            return await _portCAD.GetCounter(query);
        }
    }
}
