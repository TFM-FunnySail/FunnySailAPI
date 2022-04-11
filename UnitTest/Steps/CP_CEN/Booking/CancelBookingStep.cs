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
    class CancelBookingStep
    {
        private ScenarioContext _scenarioContext;
        private IBookingCEN _bookingCEN;
        private IBookingCAD _bookingCAD;
        private IBookingCP _bookingCP;
        private IRefundCAD _refundCAD;
        private IRefundCEN _refundCEN;
        private BookingEN _bookingEN;
        private IOwnerInvoiceCAD _ownerInvoiceCAD;
        private IOwnerInvoiceCEN _ownerInvoiceCEN;
        private IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        private IUserCAD _userCAD;
        private IDatabaseTransactionFactory _databaseTransactionFactory;
        private int _id;

        public CancelBookingStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _bookingCAD = new BookingCAD(applicationDbContextFake._dbContextFake);
            _refundCAD = new RefundCAD(applicationDbContextFake._dbContextFake);
            _bookingCEN = new BookingCEN(_bookingCAD,null,null,null);
            _refundCEN = new RefundCEN(_refundCAD, _bookingCEN);
            _ownerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceLineCAD = new OwnerInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceCEN = new OwnerInvoiceCEN(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _userCAD);
            _databaseTransactionFactory = new DatabaseTransactionFactory(applicationDbContextFake._dbContextFake);
            _bookingCP = new BookingCP(_bookingCEN, null, null, null, null, null, null, null, null, null, null, null, _databaseTransactionFactory, _refundCEN, null, _ownerInvoiceCEN);
        }


        [Given(@"los datos de una reserva")]
        public void GivenLosDatosDeUnaReserva()
        {
            _id = 1;
        }

        [When(@"se invoca a la función para cancelar la reserva")]
        public async void WhenSeInvocaALaFuncionParaCancelarLaReserva()
        {
            try
            {
                await _bookingCP.CancelBooking(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se cancela la reserva")]
        public async void ThenSeCancelaLaReserva()
        {
            _bookingEN = await _bookingCAD.FindById(_id);
            Assert.AreEqual(BookingStatusEnum.Cancelled, _bookingEN.Status);
        }

        [Given(@"los datos de una reserva incompletos")]
        public void GivenLosDatosDeUnaReservaIncompletos()
        {
            _id = 0;
        }

        [Then(@"no se modifica la reserva")]
        public void ThenNoSeModificaLaReserva()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Booking Id not found.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}
