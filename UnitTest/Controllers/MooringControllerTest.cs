using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Globals;
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
    public class MooringControllerTest
    {
        private MooringController _MooringController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;

        public MooringControllerTest() 
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _MooringController = new MooringController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetMoorings_ShouldReturnAllMoorings() 
        {
            var moorings = _MooringController.GetMoorings(new MooringFilters { PortId = 1 }, new Pagination());
            Assert.IsNotNull(moorings);
            Assert.AreEqual(2, moorings.Result.Value.Total);
        }

        [TestMethod]
        public void GetMooring_ShouldReturnOneMooring()
        {
            var moorings = _MooringController.GetMooring(1);
            Assert.IsNotNull(moorings);
            Assert.AreEqual(1, moorings.Result.Value.Id);
        }

        [TestMethod]
        public void PutMooringEN_ShouldReturnNotContent()
        {
            var moorings = _MooringController.PutMooringEN(1, new UpdateMooringDTO
            {
                id = 1,
                Alias = "mooringAliasNew"
            });

            Assert.IsNotNull(moorings);
            Assert.IsInstanceOfType(moorings.Result, typeof(NoContentResult));
        }


        [TestMethod]
        public void PostMooringEN_ShouldReturnId()
        {
            var moorings = _MooringController.PostMooring(new AddMooringDTO { 
                Alias = "algo",
                PortId = 1,
                Type = MooringEnum.Big
            });

            Assert.IsNotNull(moorings);
            Assert.IsInstanceOfType(moorings.Result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public void DeleteMooringEN_ShouldReturnNoContent()
        {
            var moorings = _MooringController.DeleteMooringEN(1);
            Assert.IsNotNull(moorings);
            Assert.IsInstanceOfType(moorings.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteMooringEN_ShouldReturnNotFound()
        {
            var moogins = _MooringController.DeleteMooringEN(15);
            Assert.IsNotNull(moogins);
            Assert.AreEqual(404, new NotFoundObjectResult(moogins.Result).StatusCode);
        }

        [TestMethod]
        public void PutMooring_ShouldReturnNoContent()
        {
            var mooring = _MooringController.PutMooringEN(1,
                new FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring.UpdateMooringDTO
                {
                    id = 1,
                    Alias = "mooring1",
                    PortId = 1,
                    Type = FunnySailAPI.ApplicationCore.Models.Globals.MooringEnum.Medium
                });
            Assert.IsNotNull(mooring);
            Assert.IsInstanceOfType(mooring.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteMooring_ShouldReturnNoContent()
        {
            var mooring = _MooringController.DeleteMooringEN(1);
            Assert.IsNotNull(mooring);
            Assert.IsInstanceOfType(mooring.Result, typeof(NoContentResult));
        }
    }
}
