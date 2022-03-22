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

        [Given(@"el amarre tiene como alias el Amarre (.) y id puerto el (.) y tipo (.*)")]
        public void GivenElAmarreTieneComoAliasElAmarreYIdPuertoElYTipoSmall(int idPort, string alias, MooringEnum type)
        {
            _idPort = idPort;
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

        [Then(@"devuelve el id de mooring")]
        public void ThenDevuelveElIdDeMorrig()
        {
            Assert.AreEqual(2, _id);
        }

        [Given(@"el amarre tiene como id puerto el (.*)")]
        public async void GivenElAmarreTieneComoIdPuertoEl(int idPort)
        {
            _idPort = idPort;
        }

        [Then(@"devuelve el error id no es correcto")]
        public void ThenDevuelveElErrorIdNoEsCorrecto()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                 ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Port Id cant be small than 0", ex.EnMessage);
        }

        [Given(@"el amarre tiene como id (.) y tipo (.)")]
        public async void GivenElAmarreTieneComoIdYTipoSmall(int p0, MooringEnum type)
        {
            _id = p0;
            _alias = "";
            _type = type;
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