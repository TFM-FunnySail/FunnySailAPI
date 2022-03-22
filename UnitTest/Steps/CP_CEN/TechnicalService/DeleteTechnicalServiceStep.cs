using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.TechnicalService
{
    [Binding]
    class DeleteTechnicalServiceStep
    {
        private ScenarioContext _scenarioContext;
        private ApplicationDbContext _dbContextFake;
        private ITechnicalServiceCEN _serviceCEN;
        private ITechnicalServiceCAD _serviceCAD;
        private ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        private int _idToDelete;
        private TechnicalServiceEN _tecServiceUpdated;

        public DeleteTechnicalServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _dbContextFake = applicationDbContextFake._dbContextFake;
            _serviceCAD = new TechnicalServiceCAD(_dbContextFake);
            _technicalServiceBoatCAD = new TechnicalServiceBoatCAD(_dbContextFake);
            _serviceCEN = new TechnicalServiceCEN(_serviceCAD, _technicalServiceBoatCAD);

        }

        [Given(@"se quiere eliminar el servicio de id (.*)")]
        public void GivenSeQuiereEliminarElServicioDeIdQueNoExiste(int id)
        {
            _idToDelete = id;
        }

        [Given(@"se quiere eliminar el servicio de id (.*), que no tiene barcos")]
        public void GivenSeQuiereEliminarElServicioDeIdQueNoTieneBarcos(int id)
        {
            _idToDelete = id;
            _dbContextFake.Add<TechnicalServiceEN>(new TechnicalServiceEN
            {
                Active = true,
                Description = "prueba",
                Price = 20,
                Id = id
            });
        }

        [Given(@"se quiere eliminar el servicio (.*) con barcos utilizandolo")]
        public void GivenSeQuiereEliminarElServicioConBarcosUtilizandolo(int id)
        {
            _idToDelete = id;
        }

        [When(@"se intenta eliminar")]
        public async Task WhenSeIntentaEliminar()
        {
            try
            {
                await _serviceCEN.DeleteService(_idToDelete);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception", ex);
            }
        }

        [Then(@"devuelve una excepcion de tipo not found")]
        public void ThenDevuelveUnaExcepcionDeTipoNotFound()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

        [When(@"se recupera el servicio actualizado para comprobar su estado")]
        public async Task WhenSeRecuperaElServicioActualizadoParaComprobarSuEstado()
        {
            _tecServiceUpdated = await _serviceCAD.FindById(_idToDelete);
        }

        [Then(@"el servicio técnico está desactivado")]
        public void ThenElServicioTecnicoEstaDesactivado()
        {
            Assert.IsFalse(_tecServiceUpdated.Active);
        }

        [Then(@"el servicio técnico no existe")]
        public void ThenElServicioTecnicoNoExiste()
        {
            Assert.IsNull(_tecServiceUpdated);
        }

    }
}
