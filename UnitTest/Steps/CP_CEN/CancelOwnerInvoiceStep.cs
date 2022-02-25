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

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    class CancelOwnerInvoiceStep
    {
        private ScenarioContext _scenarioContext;
        private IOwnerInvoiceCEN _ownerInvoiceCEN;
        private IOwnerInvoiceCAD _ownerInvoiceCAD;
        private IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        private OwnerInvoiceEN _OwnerInvoiceEN;
        private int _id;

        public CancelOwnerInvoiceStep(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _ownerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceLineCAD = new OwnerInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceCEN = new OwnerInvoiceCEN(_ownerInvoiceCAD, _ownerInvoiceLineCAD);     
        }

        [Given(@"que se quiere cancelar una factura de un propietario id (.*)")]
        public void GivenQueSeQuiereCancelarUnaFacturaDeUnPropietario(int id)
        {
            _id = id;
        }

        [When(@"se cancela un servicio")]
        public async Task WhenSeCancelaUnServicio()
        {
            try
            {
               _OwnerInvoiceEN = await _ownerInvoiceCEN.CancelOwnerInvoice(_id);
            }
            catch (DataValidationException ex) 
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se cancela la factura")]
        public void ThenSeCancelaLaFactura()
        {
            Assert.AreEqual(_OwnerInvoiceEN.IsCanceled, true);
        }

        [When(@"No se encuentra la factura")]
        public async void WhenNoSeEncuentraLaFactura()
        {
            try
            {
                _OwnerInvoiceEN = await _ownerInvoiceCEN.CancelOwnerInvoice(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"devuelve un error de que no se ha encontrado la factura")]
        public void ThenDevuelveUnErrorDeQueNoSeHaEncontradoLaFactura()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Owner invoice not found.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);

        }

    }
}
