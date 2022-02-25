using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
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
    class DeletePortStep
    {
        private ScenarioContext _scenarioContext;
        private IPortCEN _portCEN;
        private IPortCAD _portCAD;
        private PortEN _portEN;
        private int _id;
        private Task _task;

        public DeletePortStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _portCAD = new PortCAD(applicationDbContextFake._dbContextFake);
            _portCEN = new PortCEN(_portCAD);
        }

        [Given(@"un puerto con su identificador (.*)")]
        public void GivenUnPuertoConSuIdentificador(int id)
        {
            _id = id;
        }

        [When(@"se procede a la eliminación")]
        public async void WhenSeProcedeALaEliminacion()
        {
            try
            {
                await _portCEN.DeletePort(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se elimina el puerto correctamente")]
        public void ThenSeEliminaElPuertoCorrectamente()
        {
             Assert.AreEqual(_task, null);
        }

        [Given(@"un puerto que aún tiene que ofrecer servicios a clientes")]
        public void GivenUnPuertoQueAunTieneQueOfrecerServiciosAClientes()
        {
            _id = -1;
        }

        [When(@"se intente eliminar el puerto")]
        public async void WhenSeIntenteEliminarElPuerto()
        {
            try
            {
                await _portCEN.DeletePort(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"no se podrá proceder con la eliminación")]
        public void ThenNoSePodraProcederConLaEliminacion()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Port not found.", ex.EnMessage);
        }

    }
}
