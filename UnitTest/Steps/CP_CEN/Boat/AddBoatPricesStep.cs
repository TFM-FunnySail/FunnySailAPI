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
        private int _boatId;
        private decimal _dayBasePrice;
        private decimal _hourBasePrice;
        private float _supplement;

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
            _boatId = 0;
        }

        [When(@"se quiere indicar los precios asignados a una embarcación")]
        public async void WhenSeQuiereIndicarLosPreciosAsignadosAUnaEmbarcacion()
        {
            try
            {
                _id = await _boatPricesCEN.AddBoatPrices(new BoatPricesEN {
                    BoatId = _boatId,
                    DayBasePrice = _dayBasePrice,
                    HourBasePrice = _hourBasePrice,
                    Supplement = _supplement
                });
                _boatPricesEN = await _boatPricesCAD.FindById(_id);
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

        [Given(@"un grupo de datos")]
        public void GivenUnGrupoDeDatos()
        {
            //Se declaran los parámetros de la instancia
            _boatId = 1;
            _dayBasePrice = (decimal)210.50;
            _hourBasePrice = (decimal)75.20;
            _supplement = (float)20.50;
        }

        [Then(@"se asocia el precio al barco")]
        public void ThenSeAsociaElPrecioAlBarco()
        {
            //Se comprueban los id, si todo ha ido bien, se corresponderán(?)
            Assert.AreEqual(_boatId, _boatPricesEN.BoatId);
            Assert.AreEqual(_dayBasePrice, _boatPricesEN.DayBasePrice);
            Assert.AreEqual(_hourBasePrice, _boatPricesEN.HourBasePrice);
            Assert.AreEqual(_supplement, _boatPricesEN.Supplement);
        }


    }
}