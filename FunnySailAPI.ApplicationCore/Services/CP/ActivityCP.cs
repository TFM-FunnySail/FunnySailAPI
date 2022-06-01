using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class ActivityCP : IActivityCP
    {
        private readonly IResourcesCEN _resourcesCEN;
        private readonly IActivityResourceCEN _activityResourceCEN;
        private readonly IActivityCEN _activityCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public ActivityCP(IResourcesCEN resourcesCEN,
                        IDatabaseTransactionFactory databaseTransactionFactory,
                        IActivityResourceCEN activityResourceCEN,
                        IActivityCEN activityCEN)
        {
            _resourcesCEN = resourcesCEN;
            _databaseTransactionFactory = databaseTransactionFactory;
            _activityResourceCEN = activityResourceCEN;
            _activityCEN = activityCEN;
        }

        public async Task<int> AddImage(int id, IFormFile image, bool main)
        {
            ActivityEN dbActivity = await _activityCEN.GetActivityCAD().FindById(id);
            if (dbActivity == null)
                throw new DataValidationException("Activity", "La actividad", ExceptionTypesEnum.NotFound);

            string[] extensions = new string[] { "png", "jpg" };
            if (!extensions.Any(x => image.FileName.ToLower().Contains(x)))
                throw new DataValidationException("The image file does not have the required extension",
                    "El archivo imagen no tiene la extensión requerida");

            int idResource = 0;
            string uri = await _resourcesCEN.UploadImage(image);

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    idResource = await _resourcesCEN.AddResources(main, ResourcesEnum.Image, uri);

                    await _activityResourceCEN.AddActivityResource(new ActivityResourcesEN
                    {
                        ActivityId = id,
                        ResourceId = idResource
                    });

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return idResource;
        }
    }
}
