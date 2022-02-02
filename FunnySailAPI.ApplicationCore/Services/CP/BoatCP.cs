using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Output;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
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

        public BoatCP(IBoatCEN boatCEN,
                      IBoatInfoCEN boatInfoCEN,
                      IBoatTypeCEN boatTypeCEN,
                      IBoatResourceCEN boatResourceCEN,
                      IBoatPricesCEN boatPricesCEN,
                      IRequiredBoatTitlesCEN requiredBoatTitlesCEN)
        {
            _boatCEN = boatCEN;
            _boatInfoCEN = boatInfoCEN;
            _boatPricesCEN = boatPricesCEN;
            _boatResourceCEN = boatResourceCEN;
            _boatTypeCEN = boatTypeCEN;
            _requiredBoatTitlesCEN = requiredBoatTitlesCEN;
        }

        public async Task<BoatOutputDTO> CreateBoat(AddBoatInputDTO addBoatInput)
        {
            //Validar algunos datos

            //Abrir transaccion
            int boatId = 0;
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
                foreach(AddBoatResourcesInputDTO boatResource in addBoatInput.BoatResources)
                {
                    await _boatResourceCEN.AddBoatResource(new BoatResourceEN
                    {
                        BoatId = boatId,
                        Main = boatResource.Main,
                        Type = boatResource.Type,
                        Uri = boatResource.Uri
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
            }
            catch (Exception ex)
            {

                throw;
            }

            BoatEN boatEN = await _boatCEN.GetAllDataBoat(boatId);
            return ConvertToBoatOutpuDTO(boatEN);
        }

        private BoatOutputDTO ConvertToBoatOutpuDTO(BoatEN boatEN)
        {
            return new BoatOutputDTO(boatEN);
        }

        private BoatOutputDTO ConvertToBoatOutpuDTOOld(BoatEN boatEN)
        {
            return new BoatOutputDTO
            {
                Id = boatEN.Id,
                Supplement = boatEN.BoatPrices.Supplement,
                BoatResources = boatEN.BoatResources.Select(x => new BoatResourcesOutputDTO
                {
                    Main = x.Main,
                    Type = x.Type,
                    Uri = x.Uri
                }).ToList(),
                BoatType = new BoatTypeOutputDTO
                {
                    Id = boatEN.BoatType.Id,
                    Name = boatEN.BoatType.Name,
                    Description = boatEN.BoatType.Description,
                },
                Capacity = boatEN.BoatInfo.Capacity,
                Description = boatEN.BoatInfo.Description,
                Name = boatEN.BoatInfo.Name,
                MooringPoint = boatEN.BoatInfo.MooringPoint,
                MotorPower = boatEN.BoatInfo.MotorPower,
                Length = boatEN.BoatInfo.Length,
                Sleeve = boatEN.BoatInfo.Sleeve,
                Registration = boatEN.BoatInfo.Registration,
                DayBasePrice = boatEN.BoatPrices.DayBasePrice,
                HourBasePrice = boatEN.BoatPrices.HourBasePrice,
                RequiredBoatTitles = boatEN.RequiredBoatTitles.Select(x => new RequiredBoatTitleOutputDTO
                {
                    TitleId = x.TitleId
                }).ToList()
            };
        }
    }
}
