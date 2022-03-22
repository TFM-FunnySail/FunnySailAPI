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
    class AddRequireBoatTitleStep
    {
        private ScenarioContext _scenarioContext;
        private IRequiredBoatTitlesCEN _requiredBoatTitlesCEN;
        private IRequiredBoatTitleCAD _requiredBoatTitleCAD;
        private RequiredBoatTitleEN _requiredBoatTitleEN;
        private (int, BoatTiteEnum) _id;

        public AddRequireBoatTitleStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _requiredBoatTitleCAD = new RequiredBoatTitleCAD(applicationDbContextFake._dbContextFake);
            _requiredBoatTitlesCEN = new RequiredBoatTitlesCEN(_requiredBoatTitleCAD);

        }

        [Given(@"un barco que necesita una titulación para manejarlo")]
        public void GivenUnBarcoQueNecesitaUnaTitulacionParaManejarlo()
        {
            _requiredBoatTitleEN = new RequiredBoatTitleEN {
                TitleId = BoatTiteEnum.Captaincy,
                BoatId = 1
            };
        }

        [When(@"el proceso de registro del barco esté en marcha")]
        public async void WhenElProcesoDeRegistroDelBarcoEsteEnMarcha()
        {
            try
            {
                _id = await _requiredBoatTitlesCEN.AddRequiredBoatTitle(_requiredBoatTitleEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se exigirá la especificación de un título válido para la embarcación")]
        public void ThenSeExigiraLaEspecificacionDeUnTituloValidoParaLaEmbarcacion()
        {
            Assert.AreEqual((1, BoatTiteEnum.Captaincy), _id);
        }

        [Given(@"un barco que requiere titulación por ley y no se especifica")]
        public void GivenUnBarcoQueRequiereTitulacionPorLeyYNoSeEspecifica()
        {
            _requiredBoatTitleEN = new RequiredBoatTitleEN
            {
                TitleId = BoatTiteEnum.Captaincy
            };
        }

        [When(@"se quiere continuar con el proceso de registro")]
        public async void WhenSeQuiereContinuarConElProcesoDeRegistro()
        {
            try
            {
                _id = await _requiredBoatTitlesCEN.AddRequiredBoatTitle(_requiredBoatTitleEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"no se registrará el barco por falta de información")]
        public void ThenNoSeRegistraraElBarcoPorFaltaDeInformacion()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Boat id is required.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

    }
}
