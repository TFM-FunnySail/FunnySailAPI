using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Boat
{
    [Binding]
    public class CalculatePriceBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatPricesCAD _boatPricesCAD;
        private IBoatPricesCEN _boatPricesCEN;
        private BoatPricesEN _boatPrices;
        private double _hours;
        private double _days;
        private decimal _result;

        public CalculatePriceBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatPricesCAD = new BoatPricesCAD(applicationDbContextFake._dbContextFake);
            _boatPricesCEN = new BoatPricesCEN(_boatPricesCAD);

        }

        [Given(@"un barco con precio por hora (.*), un precio por dia (.*) y suplemento (.*)")]
        public void GivenUnBarcoConPrecioPorHoraUnPrecioPorDiaYSuplemento(string hourPrice, string dayPrice, string suplemento)
        {
            _boatPrices = new BoatPricesEN
            {
                BoatId = 1,
                DayBasePrice = Decimal.Parse(dayPrice),
                HourBasePrice = Decimal.Parse(hourPrice),
                Supplement = float.Parse(suplemento),
            };
        }

        [Given(@"se quiere saber el precio para las horas (.*) y dias (.*)")]
        public void GivenSeQuiereSaberElPrecioParaLasHorasYDias(string hours, string days)
        {
            _hours = Double.Parse(hours);
            _days = Double.Parse(days);
        }

        [When(@"se calcula el precio del barco")]
        public void WhenSeCalculaElPrecioDelBarco()
        {
            _result = _boatPricesCEN.CalculatePrice(_boatPrices, _days, _hours);
        }

        [Then(@"el resultado seria (.*)")]
        public void ThenElResultadoSeria(string result)
        {
            Assert.AreEqual(Decimal.Parse(result),_result);
        }

    }
}
