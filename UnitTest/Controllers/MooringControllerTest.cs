using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.Helpers;
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
    }
}
