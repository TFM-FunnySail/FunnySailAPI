using FunnySailAPI.ApplicationCore.Exceptions;
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

        public async Task DeleteMooring(int mooringId)
        {
            MooringEN dbMooring = await _mooringCAD.FindByIdAllData(mooringId,true);

            if(dbMooring == null)
                throw new DataValidationException("Mooring", "Amarre de puerto",
                    ExceptionTypesEnum.NotFound);
            if (dbMooring.Boat != null)
                throw new DataValidationException("Cannot be removed because there is a boat in mooring port",
                    "No se puede eliminar porque hay un puerto en el punto de amarre.");

            await _mooringCAD.Delete(dbMooring);
        } 
    }
}
