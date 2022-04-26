using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Sercices;
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
    public class ServicesControllerTest
    {
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;
        private ServicesController _ServicesController;

        public ServicesControllerTest()
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _ServicesController = new ServicesController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetServices_ShouldReturnAllServices()
        {
            var services = _ServicesController.GetServices(new ServiceFilters { Active = true }, new Pagination());
            Assert.IsNotNull(services);
            Assert.AreEqual(2, services.Result.Value.Total);
        }

        [TestMethod]
        public void GetService_ShouldReturnOneService()
        {
            var services = _ServicesController.GetService(1);
            Assert.IsNotNull(services);
            Assert.AreEqual(1, services.Result.Value.Id);
        }

        [TestMethod]
        public void PutServiceEN_ShouldReturnNoContent()
        {
            var services = _ServicesController.PutServiceEN(1, new UpdateServiceDTO {
                Id = 1,
                Name = "nombre nuevo",
                Description = "description",
                Price = (decimal)100,
                Active = false
            });

            Assert.IsNotNull(services);
            Assert.IsInstanceOfType(services.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PostServices_ShouldReturnCreatedAction()
        {
            var services = _ServicesController.PostServices(new AddServiceDTO
            {
                Name = "nombre",
                Description = "Description",
                Price = (decimal) 100
            });

            Assert.IsNotNull(services);
            Assert.IsInstanceOfType(services.Result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void DeleteService_ShouldReturnNoContent()
        {
            var services = _ServicesController.DeleteService(1);
            Assert.IsNotNull(services);
            Assert.IsInstanceOfType(services.Result, typeof(NoContentResult));
        }
    }
}
