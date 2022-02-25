using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;

namespace UnitTest.Steps.CP_CEN.Services
{
    [Binding]
    public class DeleteServiceStep
    {
        private ScenarioContext _scenarioContext;
        private IServiceCEN _serviceCEN;
        private IServiceCAD _serviceCAD;
        private IServiceBookingCAD _serviceBookingCAD;
        private ServiceEN _deletedService;
        private int _id;

        public DeleteServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _serviceCAD = new ServiceCAD(applicationDbContextFake._dbContextFake);
            _serviceBookingCAD = new ServiceBookingCAD(applicationDbContextFake._dbContextFake);
            _serviceCEN = new ServiceCEN(_serviceCAD, _serviceBookingCAD);

        }

        [Given(@"se pretende borrar el servicio con id: (.*) que no tiene ordenes")]
        public void GivenSePretendeBorrarElServicioConIdQueNoTieneOrdenes(int id)
        {
            _id = id;
        }
        
        [Given(@"se pretende borrar el servicio con id: (.*) que tiene ordenes")]
        public void GivenSePretendeBorrarElServicioConIdQueTieneOrdenes(int id)
        {
            _id = id;
        }
        
        [When(@"se intenta eliminar el servicio")]
        public async Task WhenSeIntentaEliminarElServicioAsync()
        {
            try
            {
                await _serviceCEN.DeleteService(_id);
                _deletedService = await _serviceCAD.FindById(_id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception", ex);
            }
        }
        
        [Then(@"devuelve el objeto borrado vacio")]
        public void ThenDevuelveElObjetoBorradoVacio()
        {
            Assert.AreEqual(_deletedService, null);
        }
        
        [Then(@"devuelve un objeto sin activar")]
        public void ThenDevuelveUnObjetoSinActivar()
        {
            Assert.AreEqual(_deletedService.Active, false);
        }
    }
}
