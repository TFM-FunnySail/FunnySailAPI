using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class MooringCEN : IMooringCEN
    {
        private readonly IMooringCAD _mooringCAD;

        public MooringCEN(IMooringCAD mooringCAD)
        {
            _mooringCAD = mooringCAD;
        }

        public async Task<int> AddMooring(int portId,string alias, MooringEnum type)
        {
            MooringEN dbMooring = await _mooringCAD.AddAsync(new MooringEN
            {
                Alias = alias,
                PortId = portId,
                Type = type
            });

            return dbMooring.Id;
        }
    }
}
