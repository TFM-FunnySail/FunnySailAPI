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
    public class ApproveBoatStep
    {
        private ScenarioContext _scenarioContext;
        private IBoatCEN _boatCEN;
        private IBoatCAD _boatCAD;
        private BoatEN _boatUpdated;
        private int _id;

        public ApproveBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(_boatCAD);

        }

        [Given(@"que se quiere actualizar el barco de id (.*) y no existe")]
        public void GivenQueSeQuiereActualizarElBarcoDeIdYNoExiste(int id)
        {
            _id = id;
        }

        [When(@"se aprueba el barco")]
        public async Task WhenSeApruebaElBarco()
        {
            try
            {
                _boatUpdated = await _boatCEN.ApproveBoat(_id);
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

        [Given(@"que se quiere actualizar el barco de id (.*)")]
        public void GivenQueSeQuiereActualizarElBarcoDeId(int id)
        {
            _id = id;
        }

        [Then(@"devuelve el barco aprobado")]
        public void ThenDevuelveElBarcoAprobado()
        {
            Assert.AreEqual(_boatUpdated.Active,true);
            Assert.AreEqual(_boatUpdated.PendingToReview,false);
        }

    }
}
