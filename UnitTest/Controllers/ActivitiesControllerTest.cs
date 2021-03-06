using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Activity;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.DTO.Output.Activity;
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
    public class ActivitiesControllerTest
    {
        private ActivitiesController _ActivitiesController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;

        public ActivitiesControllerTest() {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _ActivitiesController = new ActivitiesController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetActivities_ShouldReturnAllActivities()
        {
            var _ActivitiesFilters = new ActivityFilters
            {
                Active = true
            };
            var activities = _ActivitiesController.GetActivities(_ActivitiesFilters, new Pagination());
            Assert.IsNotNull(activities);
            Assert.AreEqual(activities.Result.Value.Total, 3);
        }

        [TestMethod]
        public void GetActivity_ShouldReturnOneActivity()
        {
            var expected = new ActivityOutputDTO {
                Id = 1,
                Active = true,
                Name = "Buceo",
                Price = 330,
                Description = "Actividad de prueba 1",
                ActivityResources = new List<ActivityResourcesOutputDTO>()
            }; 
            var activities = _ActivitiesController.GetActivity(1);
            Assert.IsNotNull(activities);
            Assert.AreEqual(expected.Id, activities.Result.Value.Id);
            Assert.AreEqual(expected.Name, activities.Result.Value.Name);
            Assert.AreEqual(expected.Active, activities.Result.Value.Active);
            Assert.AreEqual(expected.Price, activities.Result.Value.Price);
            Assert.AreEqual(expected.Description, activities.Result.Value.Description);
        }

        [TestMethod]
        public void GetActivity_ShouldReturnNotFound()
        {
            var activities = _ActivitiesController.GetActivity(24);
            Assert.AreEqual(404, new NotFoundObjectResult(activities.Result.Result).StatusCode);
        }

        [TestMethod]
        public void GetAvailableActivities_ShouldReturnAllAvilableActivities()
        {
            var activities = _ActivitiesController.GetAvailableActivities( new DateTime(2020,2,1), new DateTime(2022, 12, 1), new Pagination());
            Assert.IsNotNull(activities);
            Assert.AreEqual(5, activities.Result.Value.Total);
        }

        [TestMethod]
        public void PutActivity_ShoulChangedActivity()
        {
            var activities = _ActivitiesController.PutActivityEN(1, new UpdateAcitivityInputDTO { 
                Id = 15, 
                Name = "actividad guapa",
                Description = "descripcion guapisima",
                Price = (decimal)12.21,
                Active = true,
                ActivityDate = DateTime.UtcNow
            });
            Assert.IsNotNull(activities);
        }


        [TestMethod]
        public void PostActivities_ShouldAddedActivity()
        {
            var activities = _ActivitiesController.PostActivities( new AddActivityInputDTO
            {
                Name = "actividad guapa",
                Description = "descripcion guapisima",
                Price = (decimal)12.21,
                Active = true
            });
            Assert.IsNotNull(activities);
        }


        [TestMethod]
        public void PutActiveActivity_ShouldActivateOneActivity()
        {
            var activities = _ActivitiesController.PutActiveActivity(1);
            Assert.IsNotNull(activities);
            Assert.IsInstanceOfType(activities.Result, typeof(NoContentResult));
        }


        [TestMethod]
        public void PutActiveActivity_ShouldRetrunNotFound()
        {
            var activities = _ActivitiesController.PutActiveActivity(1);
            Assert.IsNotNull(activities);
            Assert.AreEqual(404, new NotFoundObjectResult(activities.Result).StatusCode);
        }

        [TestMethod]
        public void PutDisapproveBoat_ShouldDesactivateOneActivity()
        {
            var activities = _ActivitiesController.PutActiveActivity(1);
            Assert.IsNotNull(activities);
            Assert.IsInstanceOfType(activities.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutActivity_ShouldReturnNoContent()
        {
            var activities = _ActivitiesController.PutActivityEN(1, new UpdateAcitivityInputDTO { 
                Id = 1, 
                Name = "actividad guapa",
                Description = "descripcion guapisima",
                Price = (decimal)12.21,
                Active = true,
                ActivityDate = DateTime.UtcNow
            });
            Assert.IsNotNull(activities);
            Assert.IsInstanceOfType(activities.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutActiveActivity_ShouldRetrunNoContent()
        {
            var activities = _ActivitiesController.PutActiveActivity(1);
            Assert.IsNotNull(activities);
            Assert.IsInstanceOfType(activities.Result, typeof(NoContentResult));
        }
    }
}
