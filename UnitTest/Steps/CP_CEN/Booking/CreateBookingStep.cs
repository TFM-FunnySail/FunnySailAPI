using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
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
    class CreateBookingStep
    {
        private ScenarioContext _scenarioContext;
        private IBookingCEN _bookingCEN;
        private IBookingCAD _bookingCAD;
        private IBookingCP _bookingCP;
        private IRefundCAD _refundCAD;
        private IRefundCEN _refundCEN;
        private IBoatCAD _boatCAD;
        private IBoatCEN _boatCEN;
        private BookingEN _bookingEN;
        private IUserCAD _userCAD;
        private IUserCEN _userCEN;
        private IClientInvoiceLineCAD _clientInvoiceLineCAD;
        private IClientInvoiceLineCEN _clientInvoiceLineCEN;
        private IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        private IOwnerInvoiceLineCEN _ownerInvoiceLineCEN;
        private IOwnerInvoiceCAD _ownerInvoiceCAD;
        private IOwnerInvoiceCEN _ownerInvoiceCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;
        private int _id;
        private AddBookingInputDTO _addBookingInputDTO;
        public CreateBookingStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _bookingCAD = new BookingCAD(applicationDbContextFake._dbContextFake);
            _refundCAD = new RefundCAD(applicationDbContextFake._dbContextFake);
            _bookingCEN = new BookingCEN(_bookingCAD, null, null, null);
            _refundCEN = new RefundCEN(_refundCAD, _bookingCEN);
            _userCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _userCEN = new UserCEN(_userCAD, null, null);
            _clientInvoiceLineCAD = new ClientInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _clientInvoiceLineCEN = new ClientInvoiceLineCEN(_clientInvoiceLineCAD);
            _ownerInvoiceLineCAD = new OwnerInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceLineCEN = new OwnerInvoiceLineCEN();
            _boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(_boatCAD);
            _ownerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _ownerInvoiceCEN = new OwnerInvoiceCEN(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _userCAD);
            _databaseTransactionFactory = new DatabaseTransactionFactory(applicationDbContextFake._dbContextFake);
            _bookingCP = new BookingCP(_bookingCEN, _userCEN, _clientInvoiceLineCEN, _ownerInvoiceLineCEN, null, null, null, null, _boatCEN, null, null, null, _databaseTransactionFactory, _refundCEN, null, _ownerInvoiceCEN);
        }

        [Given(@"se introducen los datos para una reserva")]
        public void GivenSeIntroducenLosDatosParaUnaReserva()
        {
            _addBookingInputDTO = new AddBookingInputDTO
            {
                ClientId = "1",
                BoatIds = new List<int> { 1 },
                EntryDate = DateTime.Now.AddDays(10),
                DepartureDate = DateTime.Now.AddDays(11),
                RequestCaptain = true,
                TotalPeople = 10
            };
        }

        [When(@"se invoca a la función para reservar el barco")]
        public async void WhenSeInvocaALaFuncionParaReservarElBarco()
        {
            try
            {
                _id = await _bookingCP.CreateBooking(_addBookingInputDTO);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se reserva el barco y se crea la reserva correctamente")]
        public async void ThenSeReservaElBarcoYSeCreaLaReservaCorrectamente()
        {
            _bookingEN = await _bookingCEN.GetBookingCAD().FindById(_id);
            Assert.AreEqual(_addBookingInputDTO.ClientId, _bookingEN.ClientId);
            Assert.AreEqual(_addBookingInputDTO.TotalPeople, _bookingEN.TotalPeople);
            Assert.AreEqual(_addBookingInputDTO.RequestCaptain, _bookingEN.RequestCaptain);
            Assert.AreEqual(_addBookingInputDTO.DepartureDate, _bookingEN.DepartureDate);
        }

        [Given(@"los datos para reserva incompletos")]
        public void GivenLosDatosParaReservaIncompletos()
        {
            _addBookingInputDTO = new AddBookingInputDTO
            {
                ClientId = null,
                BoatIds = new List<int> { 1 },
                EntryDate = DateTime.Now.AddDays(10),
                DepartureDate = DateTime.Now.AddDays(11),
                RequestCaptain = true,
                TotalPeople = 10
            };
        }

        [Then(@"no se completa la reserva")]
        public void ThenNoSeCompletaLaReserva()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
              ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Client Id is required.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

    }
}
