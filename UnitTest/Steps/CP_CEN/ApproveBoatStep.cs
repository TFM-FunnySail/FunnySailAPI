using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
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
    public class ApproveBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCEN _boatCEN;
        private int _id;

        public ApproveBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            IBoatCAD boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(boatCAD);

        }

        [Given(@"que se quiere actualizar el barco de id (.*) y no existe")]
        public void GivenQueSeQuiereActualizarElBarcoDeIdYNoExiste(int id)
        {
            _id = id;
        }

        [When(@"se adiciona el barco")]
        public async Task WhenSeAdicionaElBarco()
        {
            try
            {
               await  _boatCEN.ApproveBoat(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound",ex);
            }
        }

        [Then(@"devuelve un error porque el barco no existe")]
        public void ThenDevuelveUnErrorPorqueElBarcoNoExiste()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
                ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Boat not found.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}
