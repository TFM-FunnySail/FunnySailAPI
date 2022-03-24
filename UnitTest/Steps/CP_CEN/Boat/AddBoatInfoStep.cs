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
    class AddBoatInfoStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatInfoCEN _boatInfoCEN;
        private IBoatInfoCAD _boatInfoCAD;
        private BoatInfoEN _boatInfoEN;
        private int _id;

        public AddBoatInfoStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatInfoCAD = new BoatInfoCAD(applicationDbContextFake._dbContextFake);
            _boatInfoCEN = new BoatInfoCEN(_boatInfoCAD);
        }

        [Given(@"la información del barco está incompleta")]
        public void GivenLaInformacionDelBarcoEstaIncompleta()
        {
            _boatInfoEN = new BoatInfoEN();
        }

        [When(@"se introduce la informacion de la embarcación")]
        public async void WhenSeIntroduceLaInformacionDeLaEmbarcacion()
        {
            try
            {
                _id = await _boatInfoCEN.AddBoatInfo(_boatInfoEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"devuelve error por falta de datos y no se crea la embarcación")]
        public void ThenDevuelveErrorPorFaltaDeDatosYNoSeCreaLaEmbarcacion()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Boat name  is required.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

        [Given(@"un grupo de datos válidos")]
        public void GivenUnGrupoDeDatosValidos()
        {
            _boatInfoEN = new BoatInfoEN
            {
                BoatId = 1,
                Name = "nombreBote",
                Description = "descripcion del bote",
                Registration = "registro de bote",
                Length = (decimal)7.2,
                Sleeve = (decimal)5.2,
                Capacity = 15,
                MotorPower = 13
            };
        }

        [Then(@"el proceso se realiza correctamente y se añade la información")]
        public void ThenElProcesoSeRealizaCorrectamenteYSeAnadeLaInformacion()
        {
            Assert.AreEqual(1, _id);
        }

    }
}