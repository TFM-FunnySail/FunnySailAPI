using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class PortMooringCP : IPortMooringCP
    {
        private readonly IPortCEN _portCEN;
        private readonly IMooringCEN _mooringCEN;
        public PortMooringCP(IMooringCEN mooringCEN,
                             IPortCEN portCEN)
        {
            _mooringCEN = mooringCEN;
            _portCEN = portCEN;
        }

        public async Task<int> AddMooring(int portId, string alias, MooringEnum type)
        {
            if(await _portCEN.AnyPortById(portId))
            {
                throw new DataValidationException("Port","Puerto",ExceptionTypesEnum.NotFound);
            }

            return await _mooringCEN.AddMooring(portId,alias,type);
        }
    }
}
