using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class TechnicalServiceControllerTest
    {
        private TechnicalServiceController _TechnicalServiceController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;
        public TechnicalServiceControllerTest()
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _TechnicalServiceController = new TechnicalServiceController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetTechnicalService_ShouldReturnAllTechnicalServices()
        {
            var technicalServices = _TechnicalServiceController.GetTechnicalServices(new TechnicalServiceFilters { Active = true } , new Pagination());
            Assert.IsNotNull(technicalServices);
            Assert.AreEqual(1, technicalServices.Result.Value.Total);
        }


        [TestMethod]
        public void GetTechnicalService_ShouldReturnOneTechnicalServices()
        {
            var technicalServices = _TechnicalServiceController.GetTechnicalService(1);
            Assert.IsNotNull(technicalServices);
            Assert.AreEqual(1, technicalServices.Result.Value.Id);
        }


        [TestMethod]
        public void CreateTechnicalService_ShouldReturnCreatedAction()
        {
            var technicalServices = _TechnicalServiceController.CreateTechnicalService((decimal)100, "algo bien fachero");
            Assert.IsNotNull(technicalServices);
            Assert.IsInstanceOfType(technicalServices.Result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void ScheduleTechnicalServiceBoat_ShouldReturnNoContent()
        {
            var technicalServices = _TechnicalServiceController.ScheduleTechnicalServiceBoat(new ScheduleTechnicalServiceDTO {  
                BoatId = 1,
                Price = 100,
                ServiceDate = DateTime.Today,
                TechnicalServiceId = 1
            });

            Assert.IsNotNull(technicalServices);
            Assert.IsInstanceOfType(technicalServices.Result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void CancelTechnicalService_ShouldReturnNoContent()
        {
            var technicalServices = _TechnicalServiceController.CancelTechnicalService(1);
            Assert.IsNotNull(technicalServices);
            Assert.IsInstanceOfType(technicalServices.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutTechnicalService_ShouldReturnNoContent()
        {
            var technicalServices = _TechnicalServiceController.PutTechnicalService(1, new UpdateTechnicalServiceDTO { 
                Id = 1,
                Active = true,
                Description = "algonuevo"
            });
            Assert.IsNotNull(technicalServices);
            Assert.IsInstanceOfType(technicalServices.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteTechnicalService_ShouldReturnNoContent()
        {
            var technicalServices = _TechnicalServiceController.DeleteTechnicalService(1);
            Assert.IsNotNull(technicalServices);
            Assert.IsInstanceOfType(technicalServices.Result, typeof(NoContentResult));
        }
    }
}
