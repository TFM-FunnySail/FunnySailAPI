using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice;
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
    public class OwnerInvoiceControllerTest
    {
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;
        private OwnerInvoiceController _OwnerInvoiceController;

        public OwnerInvoiceControllerTest()
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _OwnerInvoiceController = new OwnerInvoiceController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetOwnerInvoices_ShouldReturnAllOwnerInvoices() 
        {
            var ownerInvoices = _OwnerInvoiceController.GetOwnerInvoices(new OwnerInvoiceFilters { IsPaid = false }, new Pagination());
            Assert.IsNotNull(ownerInvoices);
            Assert.AreEqual(2, ownerInvoices.Result.Value.Total);
        }

        [TestMethod]
        public void GetOwnerInvoice_ShouldReturnOneOwnerInvoice()
        {
            var ownerInvoices = _OwnerInvoiceController.GetOwnerInvoice(1);
            Assert.IsNotNull(ownerInvoices);
            Assert.AreEqual(1, ownerInvoices.Result.Value.Id);
        }

        [TestMethod]
        public void PutCancelOwnerInvoice_ShouldReturnNoContent() 
        {
            var ownerInvoices = _OwnerInvoiceController.PutCancelOwnerInvoice(1);
            Assert.IsNotNull(ownerInvoices);
            Assert.IsInstanceOfType(ownerInvoices.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PutPayOwnerInvoice_ShouldReturnNoContent()
        {
            var ownerInvoices = _OwnerInvoiceController.PutPayOwnerInvoice(1);
            Assert.IsNotNull(ownerInvoices);
            Assert.IsInstanceOfType(ownerInvoices.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GetOwnerInvoicesOrderPending_ShouldReturnPendingOrders()
        {
            var ownerInvoices = _OwnerInvoiceController.GetOwnerInvoicesOrderPending(new OwnerInvoiceLineFilters { OwnerId = "1" }, new Pagination());
            Assert.IsNotNull(ownerInvoices);
            Assert.AreEqual(0, ownerInvoices.Result.Value.Total);
        }

        [TestMethod]
        public void GetOwnerWithInvoicesOrderPending_ShouldReturnOwnersInvoices() 
        {
            var ownerInvoices = _OwnerInvoiceController.GetOwnerWithInvoicesOrderPending();
            Assert.IsNotNull(ownerInvoices);
            Assert.IsNotNull(ownerInvoices.Result.Result);
            Assert.IsInstanceOfType(ownerInvoices.Result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void PostOwnerInvoice_ShouldCreateOwnerInvoice()
        {
            var ownerInvoice = _OwnerInvoiceController.PostOwnerInvoice(new AddOwnerInvoiceInputDTO
            {
                OwnerId = "1",
                Type = OwnerInvoicesEnum.TechnicalService,
                InvoiceLinesIds = new List<int> { 1 }
            });

            Assert.IsNotNull(ownerInvoice);
            Assert.IsInstanceOfType(ownerInvoice.Result.Result, typeof(CreatedAtActionResult));
        }


    }
}
