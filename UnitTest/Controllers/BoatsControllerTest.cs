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
        public void PostBoat_ShouldAddBoat()
        {
            string[] roles = {"ADMIN"};
            var user = _UnitOfWorkFake.unitOfWork.UserCEN.AddRole("1", roles);

            var boat = _BoatsController.PostBoat(new AddBoatInputDTO {
                Name = "Boat Fachero",
                Description = "Una descripción guapa",
                Registration = "no se que esto",
                MooringPoint = "uno punto de amarre",
                Length = (decimal)15.3,
                Sleeve = (decimal)12.2,
                Capacity = 12,
                MotorPower = 10,
                BoatTypeId = 1,
                DayBasePrice = (decimal)122.3,
                Supplement = (float)12.3,
                MooringId = 1,
                OwnerId = "1"
            });

            Assert.IsNotNull(boat);
            Assert.IsNotNull(boat.Result.Value.Id);
        }

        [TestMethod]
        public void PutApproveBoat_ShouldApproveOneBoat()
        {
            var boat = _BoatsController.PutApproveBoat(1);
            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
            //Assert.AreEqual("Microsoft.AspNetCore.Mvc.NoContentResult",boat.Result.ToString());
        }

        [TestMethod]
        public void PutDisapproveBoat_ShouldDisapproveOneBoat()
        {
            var boat = _BoatsController.PutDisapproveBoat(1, new DisapproveBoatInputDTO { AdminId = "1", Observation = "fallo tecnico" });
            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
            //Assert.AreEqual("Microsoft.AspNetCore.Mvc.NoContentResult", boat.Result.ToString());
        }

        [TestMethod]
        public void PostUploadImage_ShouldUploadOneImage()
        {
            IFormFile formFile = new FormFile(null, 1, 1, "algo", "algo.png");
            var boat = _BoatsController.PostUploadImage(1, formFile, true);
            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
            //Assert.AreEqual("Microsoft.AspNetCore.Mvc.NoContentResult", boat.Result.ToString());
        }

        [TestMethod]
        public void DeleteBoatImage_ShouldRemoveImage() 
        {
            var boat = _BoatsController.DeleteBoatImage(1, 4);
            Assert.IsNotNull(boat);
            Assert.IsInstanceOfType(boat.Result, typeof(NoContentResult));
            //Assert.AreEqual("Microsoft.AspNetCore.Mvc.NoContentResult", boat.Result.ToString());
        }

        [TestMethod]
        public void GetRequiredTitles_ShouldReturnRequiredTitles()
        {
            var titles = _BoatsController.GetRequiredTitles();
            Assert.IsNotNull(titles);
            Assert.AreEqual("", titles.Result);
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
