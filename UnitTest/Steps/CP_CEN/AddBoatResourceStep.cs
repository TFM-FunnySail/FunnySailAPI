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
    class AddBoatResourceStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatResourceCEN _boatResourceCEN;
        private IBoatResourceCAD _boatResourceCAD;
        private BoatResourceEN _boatResourceEN;
        private (int, int) _ids;

        public AddBoatResourceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatResourceCAD = new BoatResourceCAD(applicationDbContextFake._dbContextFake);
            _boatResourceCEN = new BoatResourceCEN(_boatResourceCAD);

        }

        [Given(@"un identificador de embarcación y otro de recurso")]
        public void GivenUnIdentificadorDeEmbarcacionYOtroDeRecurso()
        {
            _boatResourceEN = new BoatResourceEN 
            {
                BoatId = 1, 
                ResourceId = 1
            };
        }

        [When(@"se introducen los datos")]
        public async void WhenSeIntroducenLosDatos()
        {
            try
            {
                _ids = await _boatResourceCEN.AddBoatResource(_boatResourceEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se aceptan los datos introducidos")]
        public void ThenSeAceptanLosDatosIntroducidos()
        {
            Assert.AreEqual(_ids, (1, 1));
        }

        [Given(@"unos datos incompletos")]
        public void GivenUnosDatosIncompletos()
        {
            _boatResourceEN = new BoatResourceEN
            {
                ResourceId = 1
            };
        }

        [When(@"se trata de realizar la relación")]
        public async void WhenSeTrataDeRealizarLaRelacion()
        {
            try
            {
                _ids = await _boatResourceCEN.AddBoatResource(_boatResourceEN);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"no se produce la relación por falta de datos")]
        public void ThenNoSeProduceLaRelacionPorFaltaDeDatos()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Boat id is required.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

    }
}
