using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
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
        private readonly IMooringCEN _mooringCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public BoatCP(IBoatCEN boatCEN,
                      IBoatInfoCEN boatInfoCEN,
                      IBoatTypeCEN boatTypeCEN,
                      IBoatResourceCEN boatResourceCEN,
                      IBoatPricesCEN boatPricesCEN,
                      IRequiredBoatTitlesCEN requiredBoatTitlesCEN,
                      IDatabaseTransactionFactory databaseTransactionFactory,
                      IReviewCEN reviewCEN,
                      IUserCEN userCEN,
                      IMooringCEN mooringCEN)
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
            _mooringCEN = mooringCEN;
        }

        public async Task<int> CreateBoat(AddBoatInputDTO addBoatInput)
        {
            int boatId = 0;

            //Validar algunos datos, Las excepciones se cambiaran por una de aplicacion
            if (!(await _boatTypeCEN.AnyBoatTypeById(addBoatInput.BoatTypeId)))
                throw new DataValidationException("Boat type",
                    "El tipo de embarcación", ExceptionTypesEnum.DontExists);

            if (!(await _mooringCEN.Any(new MooringFilters
            {
                MooringId = addBoatInput.MooringId
            })))
                throw new DataValidationException("Mooring.",
                    "Amarre de puerto.", ExceptionTypesEnum.DontExists);


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
                        BoatTypeId = addBoatInput.BoatTypeId,
                        MooringId = addBoatInput.MooringId
                    });

                    //Crear datos de embarcacion
                    await _boatInfoCEN.AddBoatInfo(new BoatInfoEN
                    {
                        BoatId = boatId,
                        Capacity = addBoatInput.Capacity,
                        Description = addBoatInput.Description,
                        Length = addBoatInput.Length,
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

                    //Adicionar recursos de embarcacion
                    foreach (int resourceId in addBoatInput.ResourcesIdList)
                    {
                        await _boatResourceCEN.AddBoatResource(new BoatResourceEN { 
                            BoatId = boatId,
                            ResourceId = resourceId
                        });
                    }

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

        public async Task<BoatEN> DisapproveBoat(int boatId, DisapproveBoatInputDTO disapproveBoatInput)
        {
            BoatEN dbBoat = await _boatCEN.GetBoatCAD().FindById(boatId);
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
                    await _boatCEN.DisapproveBoat(boatId);

                    //Adicionar revision
                    int newReview = await _reviewCEN.AddReview(boatId, disapproveBoatInput.AdminId,
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

        public Task<decimal> CalculatePrice()
        {
            throw new NotImplementedException();
        }

        public async Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput)
        {
            BoatEN boat = null;
            //Validar algunos datos, Las excepciones se cambiaran por una de aplicacion
            if(updateBoatInput.BoatTypeId != null)
            {
                if (!(await _boatTypeCEN.AnyBoatTypeById((int)updateBoatInput.BoatTypeId)))
                    throw new DataValidationException("Boat type",
                    "El tipo de embarcación", ExceptionTypesEnum.DontExists);
            }

            if (updateBoatInput.MooringId != null)
            {
                if (!(await _mooringCEN.Any( new MooringFilters { 
                    MooringId = updateBoatInput.MooringId
                })))
                    throw new DataValidationException("Mooring.",
                        "Amarre de puerto.", ExceptionTypesEnum.DontExists);
            }

            //Abrir transaccion
            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    boat = await _boatCEN.UpdateBoat(updateBoatInput);

                    if(updateBoatInput.BoatInfo != null)
                    {
                        updateBoatInput.BoatInfo.BoatId = updateBoatInput.BoatId;
                        boat.BoatInfo = await _boatInfoCEN.UpdateBoat(updateBoatInput.BoatInfo);
                    }

                    if (updateBoatInput.Prices != null)
                    {
                        updateBoatInput.Prices.BoatId = updateBoatInput.BoatId;
                        boat.BoatPrices = await _boatPricesCEN.UpdateBoat(updateBoatInput.Prices);
                    }

                    if (updateBoatInput.RequiredTitles != null)
                    {
                        updateBoatInput.RequiredTitles.BoatId = updateBoatInput.BoatId;
                        await _requiredBoatTitlesCEN.UpdateRequiredBoatTitle(updateBoatInput.RequiredTitles);
                    }

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return boat;
        }
    }
}
