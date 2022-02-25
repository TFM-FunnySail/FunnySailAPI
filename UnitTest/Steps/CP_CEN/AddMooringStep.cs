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
    class AddMooringStep
    {
        private ScenarioContext _scenarioContext;
        private IMooringCEN _MooringCEN;
        private IMooringCAD _MooringCAD;
        private MooringEN _MooringEN;
        private int _idPort;
        private string _alias;
        private MooringEnum _type;
        private int _id;

        public AddMooringStep(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _MooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _MooringCEN = new MooringCEN(_MooringCAD);
        }

        [Given(@"el amarre tiene como id puerto el (.*) y alias el Amarre(.*) y tipo (.*)")]
        public void GivenElAmarreTieneComoIdPuertoElYAliasElAmarreYTipoSmall(int idport, string alias, MooringEnum type)
        {
            _idPort = idport;
            _alias = alias;
            _type = type;
        }

        [When(@"se anyade el amarre")]
        public async void WhenSeAnyadeElAmarre()
        {
            try 
            {
                _id = await _MooringCEN.AddMooring(_idPort, _alias, _type);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"devuelve el id de morrig")]
        public void ThenDevuelveElIdDeMorrig()
        {
            Assert.AreEqual(2, _id);
        }

        [Given(@"el amarre tiene como id puerto el (.*)")]
        public async void GivenElAmarreTieneComoIdPuertoEl(int p0)
        {
            try
            {
                _id = await _MooringCEN.AddMooring(-1, _alias, _type);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"devuelve el error id no es correcto")]
        public void ThenDevuelveElErrorIdNoEsCorrecto()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                 ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Port Id cant be small than 0", ex.EnMessage);
        }

        [Given(@"el amarre tiene como id (.*) y tipo small")]
        public async void GivenElAmarreTieneComoIdYTipoSmall(int p0)
        {
            try
            {
                _id = await _MooringCEN.AddMooring(_id, "", _type);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"devuelve el error alias es requerido")]
        public void ThenDevuelveElErrorAliasEsRequerido()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                 ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("the alias is required.", ex.EnMessage);
        }

    }
}
