using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.DTO.Output.Boat;
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
        public void GetBoat_ShouldReturnOneBoat()
        {
            var expected = new BoatOutputDTO {
                Id = 1,
                PendingToReview = true,
                Active = false
            };

            var boat = _BoatsController.GetBoat(1);
            Assert.IsNotNull(boat);
            Assert.AreEqual(expected.Id, boat.Result.Value.Id);
            Assert.AreEqual(expected.PendingToReview, boat.Result.Value.PendingToReview);
            Assert.AreEqual(expected.Active, boat.Result.Value.Active);
        }
    }
}
