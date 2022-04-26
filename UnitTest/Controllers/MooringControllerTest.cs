using FunnySailAPI.ApplicationCore.Interfaces;
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
