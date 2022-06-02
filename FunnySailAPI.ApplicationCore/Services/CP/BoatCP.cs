using FunnySailAPI.ApplicationCore.Constants;
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
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IResourcesCEN _resourcesCEN;
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
                      IMooringCEN mooringCEN,
                      IResourcesCEN resourcesCEN)
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
            _resourcesCEN = resourcesCEN;
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
                        MooringId = addBoatInput.MooringId,
                        OwnerId = addBoatInput.OwnerId
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
                        Supplement = addBoatInput.Supplement,
                        PorcentPriceOwner = (float)0.2
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
                    foreach (int requiredBoatTitle in addBoatInput.RequiredBoatTitles)
                    {
                        await _requiredBoatTitlesCEN.AddRequiredBoatTitle(new RequiredBoatTitleEN
                        {
                            BoatId = boatId,
                            TitleId = requiredBoatTitle
                        });
                    }

                    await _userCEN.AddRole(addBoatInput.OwnerId, new[] { UserRolesConstant.BOAT_OWNER});

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

        public async Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput)
        {
            BoatEN boat = null;
            //Validar algunos datos, Las excepciones se cambiaran por una de aplicacion
            if (updateBoatInput.MooringId != null)
            {
                MooringEN dbMooring = await _mooringCEN.GetMooringCAD().FindById((int)updateBoatInput.MooringId);
                if (dbMooring == null)
                    throw new DataValidationException("Mooring.",
                        "Amarre de puerto.", ExceptionTypesEnum.DontExists);
            }

            if (updateBoatInput.BoatTypeId != null)
            {
                 if(!(await _boatTypeCEN.AnyBoatTypeById((int)updateBoatInput.BoatTypeId)))
                    throw new DataValidationException("BoatType",
                        "Tipo de embarcación.", ExceptionTypesEnum.DontExists);
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

        public async Task<int> AddImage(int boatId, IFormFile image, bool main)
        {
            BoatEN dbBoat = await _boatCEN.GetBoatCAD().FindById(boatId);
            if (dbBoat == null)
                throw new DataValidationException("Boat", "La embarcación", ExceptionTypesEnum.NotFound);

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
                    idResource = await _resourcesCEN.AddResources(main,ResourcesEnum.Image, uri);

                    await _boatResourceCEN.AddBoatResource(new BoatResourceEN
                    {
                        BoatId = boatId,
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

        public async Task RemoveImage(int boatId, int resourceId,ApplicationUser user,IList<string> roles)
        {
            BoatEN boat = (await _boatCEN.GetAll(
                    filters: new BoatFilters { BoatId = boatId },
                    pagination: new Pagination { Limit=1, Offset = 0},
                    includeProperties: source => source
                                        .Include(x => x.BoatResources)
                                        .ThenInclude(x => x.Resource)))
                    .FirstOrDefault();

            if (boat == null)
                throw new DataValidationException("Boat", "La embarcación", ExceptionTypesEnum.NotFound);

            BoatResourceEN boatResource = boat.BoatResources.FirstOrDefault(x=> x.ResourceId == resourceId);
            if (boatResource == null)
                throw new DataValidationException("Boat resource", "Recurso", ExceptionTypesEnum.NotFound);

            if (boat.BoatResources.Count(x => x.ResourceId != resourceId) == 0)
                throw new DataValidationException("The resource is the only one in the product, it cannot be deleted",
                    "El recurso es el único en el producto, no se puede eliminar");

            if(boat.OwnerId != user.Id && !roles.Contains(UserRolesConstant.ADMIN))
                throw new DataValidationException("", "", ExceptionTypesEnum.Forbidden);

            bool otherBoatWithSameResource = (await _boatResourceCEN.GetAll(
                filters: new BoatResourceFilters
                {
                    ResourceId = resourceId,
                    NotBoatId = new List<int> { boatId }
                },
                pagination: new Pagination { Limit=1, Offset = 0})).Any();

            ResourcesEN resource = boatResource.Resource;

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    await _boatResourceCEN.GetBoatResourceCAD().Delete(boatResource);

                    if(!otherBoatWithSameResource)
                        await _resourcesCEN.DeleteResource(resource);
                    
                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
