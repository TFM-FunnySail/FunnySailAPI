using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CAD
{
    [Binding]
    public class BoatTypePersistenceStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatTypeCAD _boatTypeCAD;
        private BoatTypeEN _newBoatType;
        private string _name;
        private string _description;
        public BoatTypePersistenceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatTypeCAD = new BoatTypeCAD(applicationDbContextFake._dbContextFake);

        }

        [Given(@"con nombre y descripción (.*),(.*)")]
        public void GivenConNombreYDescripcion(string name, string description)
        {
            _name = name;
            _description = description;
        }

        [When(@"se adiciona la embarcacion")]
        public async Task WhenSeAdicionaLaEmbarcacion()
        {
            try
            {
                int newBoatTypeId = await _boatTypeCAD.AddBoatType(_name, _description);
                _newBoatType = await _boatTypeCAD.FindById(newBoatTypeId);
            }
            catch (Exception ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }

        [Then(@"devuelve la embarcación creada en base de datos con los mismos valores")]
        public void ThenDevuelveLaEmbarcacionCreadaEnBaseDeDatosConLosMismosValores()
        {
            Assert.AreEqual(_name, _newBoatType.Name);
            Assert.AreEqual(_description, _newBoatType.Description);
        }


        [Given(@"con nombre (.*), sin descripcion")]
        public void GivenConNombreSinDescripcion(string name)
        {
            _name = name;
            _description = null;
        }

        [Then(@"devuelve un error porque la descripcion es requerida")]
        public void ThenDevuelveUnErrorPorqueLaDescripcionEsRequerida()
        {
            Exception ex = _scenarioContext.Get<Exception> ("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual("The name or the description is null", ex.Message);
        }

    }
}
