using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.DTO.Output.Boat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class BoatsControllerTest
    {
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private BoatsController _BoatsController;
        public BoatsControllerTest() {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _BoatsController = new BoatsController(_UnitOfWork);
        }

        [TestMethod]
        public void GetBoats_ShouldReturnAllBoats()
        {
            var _BoatFilters = new BoatFilters
            {
                Active = true
            };
            var boats = _BoatsController.GetBoats(_BoatFilters, new Pagination());
            Assert.IsNotNull(boats);
            Assert.AreEqual(1, boats.Result.Value.Total);
        }

      
        [TestMethod]
        public void GetAvilableBoats_ShouldReturnAllAvilableBoats() 
        {
            var boats = _BoatsController.GetAvailableBoats(new DateTime(2021, 2, 2), new DateTime(2022, 12, 1), new Pagination());
            Assert.IsNotNull(boats);
            Assert.AreEqual(2, boats.Result.Value.Total);
        }

        [TestMethod]
        public void PutBoat_ShouldUptadeOneBoat()
        {
            var boat = _BoatsController.PutBoatEN(1, new UpdateBoatInputDTO {
                BoatId = 1,
                Active = false,
                PendingToReview =  true 
            });

            Assert.IsNotNull(boat);
            Assert.IsNotNull(boat.Result);
        }

        [TestMethod]
        public void PutApproveBoat_ShouldApproveOneBoat()
        {
            var boat = _BoatsController.PutApproveBoat(1);
            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutBoat_ShouldReturnNoContent()
        {
            var boat = _BoatsController.PutBoatEN(1, new UpdateBoatInputDTO
            {
                BoatId = 1,
                Active = false,
                PendingToReview = true
            });

            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
        }
    }
}
