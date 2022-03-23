using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring;
using FunnySailAPI.ApplicationCore.Models.Filters;
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
            if (portId < 0)
                throw new DataValidationException("Port Id cant be small than 0", "Id Puert no puede ser menor que 0 ");

            if (alias == "")
                throw new DataValidationException("the alias" , " el alias" , ExceptionTypesEnum.IsRequired);

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
            MooringEN dbMooring = await _mooringCAD.FindByIdAllData(mooringId);

            if(dbMooring == null)
                throw new DataValidationException("Mooring", "Amarre de puerto",
                    ExceptionTypesEnum.NotFound);

            if (await _mooringCAD.AnyBoatWithMooring(mooringId))
                throw new DataValidationException("Cannot be removed because there is a boat in mooring port",
                    "No se puede eliminar porque hay un puerto en el punto de amarre.");

            await _mooringCAD.Delete(dbMooring);
        }

        public async Task<MooringEN> UpdateMooring(UpdateMooringDTO updateMooringInput)
        {
            if(updateMooringInput.MooringId == 0)
                throw new DataValidationException("Mooring id", "Amarre de puerto",
                    ExceptionTypesEnum.IsRequired);

            MooringEN dbMooring = await _mooringCAD.FindById(updateMooringInput.MooringId);

            if (dbMooring == null)
                throw new DataValidationException("Mooring", "Amarre de puerto",
                    ExceptionTypesEnum.NotFound);

            dbMooring.Alias = updateMooringInput.Alias;
            dbMooring.PortId = updateMooringInput.PortId;
            dbMooring.Type = updateMooringInput.Type;

            await _mooringCAD.Update(dbMooring);

            return dbMooring;
        }

        public async Task<bool> Any(MooringFilters filter)
        {
            var query = _mooringCAD.GetFiltered(filter);

            return await _mooringCAD.Any(query);
        }

        public IMooringCAD GetBoatCAD()
        {
            return _mooringCAD;
        }
    }
}
