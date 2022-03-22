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

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    class AddBoatPricesStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatPricesCEN _boatPricesCEN;
        private IBoatPricesCAD _boatPricesCAD;
        private BoatPricesEN _boatPricesEN;
        private int _id;

        public AddBoatPricesStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatPricesCAD = new BoatPricesCAD(applicationDbContextFake._dbContextFake);
            _boatPricesCEN = new BoatPricesCEN(_boatPricesCAD);

        }

        [Given(@"un grupo de datos incompleto")]
        public void GivenUnGrupoDeDatosIncompleto()
        {
            _boatPricesEN = new BoatPricesEN ();
        }

        [When(@"se quiere indicar los precios asignados a una embarcación")]
        public async void WhenSeQuiereIndicarLosPreciosAsignadosAUnaEmbarcacion()
        {
            try
            {
                _id = await _boatPricesCEN.AddBoatPrices(_boatPricesEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se produce un error por no ajustarse al modelo de datos")]
        public void ThenSeProduceUnErrorPorNoAjustarseAlModeloDeDatos()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Boat id is required.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

    }
}
