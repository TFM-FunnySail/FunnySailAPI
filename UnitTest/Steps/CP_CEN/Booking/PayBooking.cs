using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Booking
{
    [Binding]
    class PayBooking
    {
        private ScenarioContext _scenarioContext;
        private IBookingCEN _bookingCEN;
        private IBookingCAD _bookingCAD;
        private IBookingCP _bookingCP;
        private IClientInvoiceCAD _clientInvoiceCAD; 
        private IClientInvoiceCEN _clientInvoiceCEN;
        private IClientInvoiceLineCAD _clientInvoiceLineCAD;
        private IClientInvoiceLineCEN _clientInvoiceLineCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        private ClientInvoiceEN _clientInvoiceEN;
        private int _id;
        private int _idClientInvoice;
        public PayBooking(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _bookingCAD = new BookingCAD(applicationDbContextFake._dbContextFake);
            _bookingCEN = new BookingCEN(_bookingCAD);
            _clientInvoiceLineCAD = new ClientInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _clientInvoiceLineCEN = new ClientInvoiceLineCEN(_clientInvoiceLineCAD);
            _clientInvoiceCAD = new ClientInvoiceCAD(applicationDbContextFake._dbContextFake);
            _clientInvoiceCEN = new ClientInvoiceCEN(_clientInvoiceCAD, _clientInvoiceLineCAD);
            _databaseTransactionFactory = new DatabaseTransactionFactory(applicationDbContextFake._dbContextFake);
            _bookingCP = new BookingCP(_bookingCEN, null, _clientInvoiceLineCEN, null, null, null, null, null, null ,null, null, _clientInvoiceCEN, _databaseTransactionFactory, null, null);
        }

        [Given(@"se pasa el identificador de una reserva")]
        public void GivenSePasaElIdentificadorDeUnaReserva()
        {
            _id = 1;
        }

        [When(@"se invoca la función para pagar la reserva")]
        public async void WhenSeInvocaLaFuncionParaPagarLaReserva()
        {
            try
            {
                _idClientInvoice =  await _bookingCP.PayBooking(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se marca la reserva como pagada")]
        public async void ThenSeMarcaLaReservaComoPagada()
        {
            _clientInvoiceEN = await _clientInvoiceCEN.GetClientInvoiceCAD().FindById(_idClientInvoice);
            Assert.AreEqual(_idClientInvoice, _clientInvoiceEN.Id);
        }

        [Given(@"se pasa un identificador inválido")]
        public void GivenSePasaUnIdentificadorInvalido()
        {
            _id = -1;
        }

        [Then(@"no se marca la reserva como pagada")]
        public void ThenNoSeMarcaLaReservaComoPagada()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}
