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
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Port;

namespace UnitTest.Steps.CP_CEN.Ports
{
    [Binding]
    public class CreatePortStep
    {
        private ScenarioContext _scenarioContext;
        private IPortCEN _portCEN;
        private IPortCAD _portCAD;
        private PortEN _newPort;
        private int _id;
        private string _location;
        private string _name;
        private AddPortInputDTO _addPortInputDTO;
        public CreatePortStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _portCAD = new PortCAD(applicationDbContextFake._dbContextFake);
            _portCEN = new PortCEN(_portCAD);

        }

        [Given(@"con un (.*) y una (.*)")]
        public void GivenConUnYUna(string name, string location)
        {
            _addPortInputDTO = new AddPortInputDTO
            {
                Name = name,
                Location = location
            };
        }
        
        [Given(@"con una (.*) y sin nombre")]
        public void GivenConUnaYSinNombre(string location)
        {
            _addPortInputDTO = new AddPortInputDTO
            {
                Name = null,
                Location = location
            };
        }
        
        [When(@"se adiciona el puerto")]
        public async Task WhenSeAdicionaElPuertoAsync()
        {
            try
            {
                int id = await _portCEN.AddPort(_addPortInputDTO);
                _newPort = await _portCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullName", ex);
            }
        }
        
        [Then(@"devuelve el puerto creado en base de datos con los mismos valores")]
        public void ThenDevuelveElPuertoCreadoEnBaseDeDatosConLosMismosValores()
        {
            Assert.AreEqual(_addPortInputDTO.Location, _newPort.Location);
            Assert.AreEqual(_addPortInputDTO.Name, _newPort.Name);
        }
        
        [Then(@"devuelve un error porque el nombre del puerto es requerido")]
        public void ThenDevuelveUnErrorPorqueElNombreDelPuertoEsRequerido()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullName");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
