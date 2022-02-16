using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Output;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP.FunnySail
{
    public class BoatCP : IBoatCP
    {
        private readonly IBoatCEN _boatCEN;
        private readonly IBoatInfoCEN _boatInfoCEN;
        private readonly IBoatPricesCEN _boatPricesCEN;
        private readonly IBoatResourceCEN _boatResourceCEN;
        private readonly IBoatTypeCEN _boatTypeCEN;
        private readonly IRequiredBoatTitlesCEN _requiredBoatTitlesCEN;
        private readonly IReviewCEN _reviewCEN;
        private readonly IUserCEN _userCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public BoatCP(IBoatCEN boatCEN,
                      IBoatInfoCEN boatInfoCEN,
                      IBoatTypeCEN boatTypeCEN,
                      IBoatResourceCEN boatResourceCEN,
                      IBoatPricesCEN boatPricesCEN,
                      IRequiredBoatTitlesCEN requiredBoatTitlesCEN,
                      IDatabaseTransactionFactory databaseTransactionFactory,
                      IReviewCEN reviewCEN,
                      IUserCEN userCEN)
        {
            _boatCEN = boatCEN;
            _boatInfoCEN = boatInfoCEN;
            _boatPricesCEN = boatPricesCEN;
            _boatResourceCEN = boatResourceCEN;
            _boatTypeCEN = boatTypeCEN;
            _requiredBoatTitlesCEN = requiredBoatTitlesCEN;
            _databaseTransactionFactory = databaseTransactionFactory;
            _reviewCEN = reviewCEN;
            _userCEN = userCEN;
        }

        public async Task<int> CreateBoat(AddBoatInputDTO addBoatInput)
        {
            int boatId = 0;

            //Validar algunos datos, Las excepciones se cambiaran por una de aplicacion
            if (!(await _boatTypeCEN.AnyBoatTypeById(addBoatInput.BoatTypeId)))
                throw new DataValidationException("Boat type not found.",
                    "El tipo de embarcación no ha sido encontrado.");

            if (!addBoatInput.BoatResources.Any(x => x.Main))
                throw new DataValidationException("Not found main resources.",
                    "No hay un recurso principal.");

            if (addBoatInput.BoatResources.Count(x => x.Main) > 1)
                throw new DataValidationException("There can only be one main resource.",
                    "Solo puede haber un recurso principal.");

            //Abrir transaccion
            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    //Crear embarcacion
                    boatId = await _boatCEN.CreateBoat(new BoatEN
                    {
                        Active = false,
                        CreatedDate = DateTime.UtcNow,
                        PendingToReview = true,
                        BoatTypeId = addBoatInput.BoatTypeId
                    });

                    //Crear datos de embarcacion
                    await _boatInfoCEN.AddBoatInfo(new BoatInfoEN
                    {
                        BoatId = boatId,
                        Capacity = addBoatInput.Capacity,
                        Description = addBoatInput.Description,
                        Length = addBoatInput.Length,
                        MooringPoint = addBoatInput.MooringPoint,
                        MotorPower = addBoatInput.MotorPower,
                        Name = addBoatInput.Name,
                        Registration = addBoatInput.Registration,
                        Sleeve = addBoatInput.Sleeve
                    });

                    //Crear precios de embarcacion
                    await _boatPricesCEN.AddBoatPrices(new BoatPricesEN
                    {
                        BoatId = boatId,
                        DayBasePrice = addBoatInput.DayBasePrice,
                        HourBasePrice = addBoatInput.HourBasePrice,
                        Supplement = addBoatInput.Supplement
                    });
                    //Crear imagenes de embarcacion
                    //foreach (AddBoatResourcesInputDTO boatResource in addBoatInput.BoatResources)
                    //{
                    //    await _boatResourceCEN.AddBoatResource(new BoatResourceEN
                    //    {
                    //        BoatId = boatId,
                    //        Main = boatResource.Main,
                    //        Type = boatResource.Type,
                    //        Uri = boatResource.Uri
                    //    });
                    //}

                    //Crear titulacion requerida
                    foreach (AddRequiredBoatTitleInputDTO requiredBoatTitle in addBoatInput.RequiredBoatTitles)
                    {
                        await _requiredBoatTitlesCEN.AddRequiredBoatTitle(new RequiredBoatTitleEN
                        {
                            BoatId = boatId,
                            TitleId = requiredBoatTitle.Title
                        });
                    }

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return boatId;
        }

        public async Task<BoatEN> DisapproveBoat(DisapproveBoatInputDTO disapproveBoatInput)
        {
            BoatEN dbBoat = await _boatCEN.GetBoatCAD().FindById(disapproveBoatInput.BoatId);
            UsersEN dbAdmin = await _userCEN.GetUserCAD().FindById(disapproveBoatInput.AdminId);
            //Validar datos
            if (dbBoat == null)
                throw new DataValidationException("Boat","La embarcación",ExceptionTypesEnum.NotFound);
            
            if(disapproveBoatInput.AdminId == null)
                throw new DataValidationException("AdminId","AdminId",ExceptionTypesEnum.IsRequired);
            
            if (disapproveBoatInput.Observation == null)
                throw new DataValidationException("Observation","Observation", ExceptionTypesEnum.IsRequired);

            if (dbAdmin == null)
                throw new DataValidationException("Admin", "El administrador", ExceptionTypesEnum.NotFound);

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    await _boatCEN.DisapproveBoat(disapproveBoatInput.BoatId);

                    //Adicionar revision
                    int newReview = await _reviewCEN.AddReview(disapproveBoatInput.BoatId, disapproveBoatInput.AdminId,
                        disapproveBoatInput.Observation);

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return dbBoat;
        }
    }
}
