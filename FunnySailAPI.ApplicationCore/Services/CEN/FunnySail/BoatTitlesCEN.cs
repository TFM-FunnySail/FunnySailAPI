using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatTitlesCEN : IBoatTitlesCEN
    {
        private readonly IBoatTitleCAD _boatTitleCAD;
        private readonly string _enName;
        private readonly string _esName;

        public BoatTitlesCEN(IBoatTitleCAD boatTitleCAD)
        {
            _boatTitleCAD = boatTitleCAD;
            _enName = "Required title";
            _esName = "requerimiento de titulación";
        }

        public IBoatTitleCAD GetBoatTitleCAD()
        {
            return _boatTitleCAD;
        }

        public async Task<int> AddBoatTitle(AddBoatTitleInputDTO addBoatTitleInput)
        {
            if (addBoatTitleInput.Name == "")
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);


            BoatTitlesEN dbBoatTitle = await _boatTitleCAD.AddAsync(new BoatTitlesEN
            {
                Name = addBoatTitleInput.Name,
                Description = addBoatTitleInput.Description
            });

            return dbBoatTitle.TitleId;
        }

        public async Task DeleteBoatTitle(int titleId)
        {
            BoatTitlesEN dbTitle = await _boatTitleCAD.FindByIdAllData(titleId);

            if (dbTitle == null)
                throw new DataValidationException("Required title", "Título requerido",
                    ExceptionTypesEnum.NotFound);

            if (await _boatTitleCAD.AnyBoatWithTitle(titleId))
                throw new DataValidationException("Cannot be removed because there is a boat whis this required title",
                    "No se puede eliminar porque hay un barco que tiene este título requerido.");

            await _boatTitleCAD.Delete(dbTitle);
        }

        public async Task<BoatTitlesEN> UpdateBoatTitle(UpdateBoatTitleDTO updateBoatTitleInput)
        {
            if (updateBoatTitleInput.TitleId == 0)
                throw new DataValidationException("Required title id", "Título requerido id",
                    ExceptionTypesEnum.IsRequired);

            BoatTitlesEN dbTitle = await _boatTitleCAD.FindById(updateBoatTitleInput.TitleId);

            if (dbTitle == null)
                throw new DataValidationException("Required title", "Título requerido",
                    ExceptionTypesEnum.NotFound);

            if (updateBoatTitleInput.Name == "")
                throw new DataValidationException($"{_enName} name", $"Nombre del {_esName}",
                    ExceptionTypesEnum.IsRequired);

            dbTitle.Name = updateBoatTitleInput.Name;
            dbTitle.Description = updateBoatTitleInput.Description;

            await _boatTitleCAD.Update(dbTitle);

            return dbTitle;
        }

        public async Task<IList<BoatTitlesEN>> GetAll(BoatTitlesFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatTitlesEN>, IOrderedQueryable<BoatTitlesEN>> orderBy = null,
            Func<IQueryable<BoatTitlesEN>, IIncludableQueryable<BoatTitlesEN, object>> includeProperties = null)
        {
            var requiredTitles = _boatTitleCAD.GetRequiredTitleFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.TitleId);
            
            return await _boatTitleCAD.Get(requiredTitles, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(BoatTitlesFilters filters = null)
        {
            var requiredTitles = _boatTitleCAD.GetRequiredTitleFiltered(filters);

            return await _boatTitleCAD.GetCounter(requiredTitles);
        }
    }
}
