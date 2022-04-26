using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.User;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private UsersController _UsersController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;
        public UsersControllerTest()
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _UsersController = new UsersController(_UnitOfWork,
                                                   _RequestUtilityService,
                                                   null);
        }

        [TestMethod]
        public void GetUsersInfo_ShouldReturnAllUserInfo()
        {
            var users = _UsersController.GetUsersInfo(new UsersFilters { BoatOwner = true }, new Pagination());
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Result.Value.Total);
        }

        [TestMethod]
        public void GetUsersEN_ShouldReturnNotFound()
        {
            var users = _UsersController.GetUsersEN("11");
            Assert.IsNotNull(users);
            Assert.AreEqual(404, new NotFoundObjectResult(users.Result.Result).StatusCode);
        }
    }
}