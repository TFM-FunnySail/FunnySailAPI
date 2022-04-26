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
    public class ClientInvoiceControllerTest
    {
        public ClientInvoiceController _ClientInvoiceController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
        private IRequestUtilityService _RequestUtilityService;

        public ClientInvoiceControllerTest() 
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _RequestUtilityService = new RequestUtilityService();
            _ClientInvoiceController = new ClientInvoiceController(_UnitOfWork, _RequestUtilityService);
        }

        [TestMethod]
        public void GetClientInvoices_ShouldReturnAllClientInvoices()
        {
            var clientInvoice = _ClientInvoiceController.GetClientInvoices(new ClientInvoiceFilters {
                MinPrice = 0,
                MaxPrice = 1000
            }, new Pagination());
            Assert.IsNotNull(clientInvoice);
            Assert.AreEqual(2, clientInvoice.Result.Value.Total);
        }


        [TestMethod]
        public void GetClientInvoice_ShouldReturnOneClientInvoice()
        {
            var clientInvoice = _ClientInvoiceController.GetClientInvoice(1);
            Assert.IsNotNull(clientInvoice);
            Assert.IsNotNull(clientInvoice.Result.Value);
            Assert.AreEqual(1, clientInvoice.Result.Value.Id);
        }


        [TestMethod]
        public void PutCancelClientInvoice_ShouldReturnNotContent() 
        {
            var clientInvoice = _ClientInvoiceController.PutCancelClientInvoice(1);
            Assert.IsNotNull(clientInvoice);
            Assert.AreEqual(new NoContentResult(), clientInvoice.Result);
        }
    }
}
