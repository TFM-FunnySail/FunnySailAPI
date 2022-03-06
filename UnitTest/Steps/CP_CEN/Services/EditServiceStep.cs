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
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;

namespace UnitTest.Steps.CP_CEN.Services
{
    [Binding]
    public class EditServiceStep
    {
        private ScenarioContext _scenarioContext;
        private IServiceCEN _serviceCEN;
        private IServiceCAD _serviceCAD;
        private IServiceBookingCAD _serviceBookingCAD;
        private ServiceEN _serviceUpdated;
        private string _description;
        private string _name;
        private int _id;

        public EditServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _serviceCAD = new ServiceCAD(applicationDbContextFake._dbContextFake);
            _serviceBookingCAD = new ServiceBookingCAD(applicationDbContextFake._dbContextFake);
            _serviceCEN = new ServiceCEN(_serviceCAD, _serviceBookingCAD);

        }

        [Given(@"se pretende editar la (.*) y (.*) del servicio que tiene la id: (.*)")]
        public void GivenSePretendeEditarLaDelServicioQueTieneLaId(string desc, string name, int id)
        {
            _id = id;
            _description = desc;
            _name = name;      
        }
        
        [Given(@"se pretende editar el servicio con (.*) y (.*) dejando el nombre vacio")]
        public void GivenSePretendeEditarElServicioConDejandoElNombreVacio(int id, string desc)
        {
            _id = id;
            _description = desc;
            _name = null;
        }
        
        [When(@"se edita el servicio")]
        public async Task WhenSeEditaElServicioAsync()
        {
            try
            {
                _serviceUpdated = await _serviceCEN.UpdateService(new UpdateServiceDTO
                {
                    Id = _id,
                    Name = _name,
                    Description = _description
                });
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullName", ex);
            }
        }
        
        [Then(@"devuelve el servicio editado en base de datos con los valores actualizados")]
        public void ThenDevuelveElServicioEditadoEnBaseDeDatosConLosValoresActualizados()
        {
            Assert.AreEqual(_serviceUpdated.Name, _name);
            Assert.AreEqual(_serviceUpdated.Description, _description);
        }
        
        [Then(@"devuelve un error porque el nombre del servicio es requerido")]
        public void ThenDevuelveUnErrorPorqueElNombreDelServicioEsRequerido()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullName");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
