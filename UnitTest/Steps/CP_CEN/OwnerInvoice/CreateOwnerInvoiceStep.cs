using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;
using OwnerInvoiceCEN = FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoiceCEN;

namespace UnitTest.Steps.CP_CEN.OwnerInvoice
{
    [Binding]
    class CreateOwnerInvoiceStep
    {
        private ScenarioContext _scenarioContext;
        private IOwnerInvoiceCEN _ownerInvoiceCEN;
        private IOwnerInvoiceCAD _ownerInvoiceCAD;
        private IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        private IUserCAD _userCAD;
        private OwnerInvoiceEN _newOwnerInvoice;
        private string _ownerId;
        private decimal _amount;
        //toCollect está mal escrito y pone toCollet
        private bool _toCollet;

        public CreateOwnerInvoiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _ownerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceLineCAD = new OwnerInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceCEN = new OwnerInvoiceCEN(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _userCAD);
        }


        [Given(@"que se quiere crear una factura y los parámetros son correctos")]
        public void GivenQueSeQuiereCrearUnaFacturaYLosParametrosSonCorrectos()
        {
            //toCollect está mal escrito y pone toCollet
            _ownerId = "1";
            _amount = (decimal)5.0;
            _toCollet = true;
        }

        [When(@"se invoca a la función")]
        public async void WhenSeInvocaALaFuncion()
        {
            try
            {
                int id = await _ownerInvoiceCEN.CreateOwnerInvoice(_ownerId, _amount, _toCollet);
                _newOwnerInvoice = await _ownerInvoiceCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }

        [Then(@"se crea la factura")]
        public void ThenSeCreaLaFactura()
        {
            Assert.AreEqual(_ownerId, _newOwnerInvoice.OwnerId);
            Assert.AreEqual(_amount, _newOwnerInvoice.Amount);
            Assert.AreEqual(_toCollet, _newOwnerInvoice.ToCollet);
        }

        [Given(@"que se quiere crear una factura con los parámetros en un formato incorrecto")]
        public void GivenQueSeQuiereCrearUnaFacturaConLosParametrosEnUnFormatoIncorrecto()
        {
            _ownerId = null;
        }

        [Then(@"no se crea la factura")]
        public void ThenNoSeCreaLaFactura()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}