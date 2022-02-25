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
    public class CreateServiceStep
    {
        private ScenarioContext _scenarioContext;
        private IServiceCEN _serviceCEN;
        private IServiceCAD _serviceCAD;
        private IServiceBookingCAD _serviceBookingCAD;
        private ServiceEN _newService;
        private decimal _price;
        private string _description;
        private string _name;
        public CreateServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _serviceCAD = new ServiceCAD(applicationDbContextFake._dbContextFake);
            _serviceBookingCAD = new ServiceBookingCAD(applicationDbContextFake._dbContextFake);
            _serviceCEN = new ServiceCEN(_serviceCAD, _serviceBookingCAD);

        }

        [Given(@"con los siguientes datos (.*), (.*) y (.*)")]
        public void GivenConLosSiguientesDatosY(string desc, string price, string name)
        {
            _description = desc;
            _price = Decimal.Parse(price);
            _name = name;
        }
        
        [Given(@"con un (.*), un (.*)")]
        public void GivenConUnUn(string name, string price)
        {
            _price = Decimal.Parse(price);
            _name = name;
        }
        
        [When(@"se adiciono el servicio")]
        public async Task WhenSeAdicionoElServicioAsync()
        {
            try
            {
                int id = await _serviceCEN.CreateService(_name, _price, _description);
                _newService = await _serviceCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }
        
        [When(@"se adiciono el servicio sin descripcion")]
        public async Task WhenSeAdicionoElServicioSinDescripcion()
        {
            try
            {
                int id = await _serviceCEN.CreateService(_name, _price, _description);
                _newService = await _serviceCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }
        
        [Then(@"devuelve el servicio creado en base de datos con los mismos valores")]
        public void ThenDevuelveElServicioCreadoEnBaseDeDatosConLosMismosValores()
        {
            Assert.AreEqual(_price, _newService.Price);
            Assert.AreEqual(_description, _newService.Description);
            Assert.AreEqual(_name, _newService.Name);
        }
        
        [Then(@"devuelve un error porque la descripcion del servicio es requerida")]
        public void ThenDevuelveUnErrorPorqueLaDescripcionDelServicioEsRequerida()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
