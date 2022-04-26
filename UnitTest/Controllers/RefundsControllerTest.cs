using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class RefundsControllerTest
    {
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private RefundsController _RefundController;

        public RefundsControllerTest() 
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RefundController = new RefundsController(_UnitOfWork);
        }

        [TestMethod]
        public void GetRefunds_ShouldReturnAllRefunds() 
        {
            var refunds = _RefundController.GetRefunds(new RefundFilters { BookingId = 1 }, new Pagination() );
            Assert.IsNotNull(refunds);
            Assert.AreEqual(2, refunds.Result.Value.Total);
        }

        [TestMethod]
        public void GetRefund_ShouldReturOneRefunds() 
        {
            var refunds = _RefundController.GetRefund(1);
            Assert.IsNotNull(refunds);
            Assert.AreEqual(1, refunds.Result.Value.Id);
        }

        [TestMethod]
        public void GetRefund_ShouldReturnNotFound()
        {
            var refunds = _RefundController.GetRefund(100);
            Assert.IsNotNull(refunds);
            Assert.AreEqual(404, new NotFoundObjectResult(refunds.Result.Result).StatusCode);
        }
    }
}
