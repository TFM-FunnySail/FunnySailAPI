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
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Refund
{
    [Binding]
    class CreateRefund
    {
        private ScenarioContext _scenarioContext;
        private IBookingCAD _bookingCAD;
        private IBookingCEN _bookingCEN;
        private IRefundCAD _refundCAD;
        private IRefundCEN _refundCEN;
        private RefundEN _refundEN;
        private int _id;
        private int _bookingId;
        private string _description;
        private decimal _amountToReturn;
        public CreateRefund(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _bookingCAD = new BookingCAD(applicationDbContextFake._dbContextFake);
            _bookingCEN = new BookingCEN(_bookingCAD,null,null,null);
            _refundCAD = new RefundCAD(applicationDbContextFake._dbContextFake);
            _refundCEN = new RefundCEN(_refundCAD, _bookingCEN);
        }

        [Given(@"un grupo de datos para tramitar una devolución")]
        public void GivenUnGrupoDeDatosParaTramitarUnaDevolucion()
        {
            _bookingId = 1;
            _description = "description";
            _amountToReturn = (decimal)50.60;
        }

        [When(@"se invoca la función de crear devolución")]
        public async void WhenSeInvocaLaFuncionDeCrearDevolucion()
        {
            try
            {
                _id = await _refundCEN.CreateRefund(_bookingId, _description, _amountToReturn);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se crea la devolución")]
        public async void ThenSeCreaLaDevolucion()
        {
            _refundEN = await _refundCEN.GetRefundCAD().FindById(_id);
            Assert.AreEqual(_bookingId, _refundEN.BookingId);
            Assert.AreEqual(_description, _refundEN.Description);
            Assert.AreEqual(_amountToReturn, _refundEN.AmountToReturn);
        }

        [Given(@"un grupo de datos para tramitar una devolución con datos incorrectos")]
        public void GivenUnGrupoDeDatosParaTramitarUnaDevolucionConDatosIncorrectos()
        {
            _bookingId = -1;
            _description = "description";
            _amountToReturn = (decimal)50.60;
        }

        [Then(@"no se crea la devolución")]
        public void ThenNoSeCreaLaDevolucion()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Ex_NotFound");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}
