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

namespace UnitTest.Steps.CP_CEN.Port
{
    [Binding]
    public class EditPortSteps
    {
        private ScenarioContext _scenarioContext;
        private IPortCEN _portCEN;
        private IPortCAD _portCAD;
        private PortEN _portUpdated;
        private string _name;
        private string _location;
        private int _id;

        public EditPortSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _portCAD = new PortCAD(applicationDbContextFake._dbContextFake);
            _portCEN = new PortCEN(_portCAD);

        }

        [Given(@"se pretende editar el (.*) y (.*) del puerto con (.*)")]
        public void GivenSePretendeEditarElDelPuertoCon(string name, string location, int id)
        {
            _name = name;
            _location = location;
            _id = id;
        }
        [Given(@"se pretende editar un puerto con (.*) y (.*) dejando la localización vacia")]
        public void GivenSePretendeEditarUnPuertoConYPuertoDeLaAmarguraDejandoLaLocalizacionVacia(int id, string location)
        {
            _id = id;
            _location = location;
            _name = null;
        }

        [When(@"se edita el puerto")]
        public async Task WhenSeEditaElPuertoAsync()
        {
            try
            {
                _portUpdated = await _portCEN.EditPort(new UpdatePortDTO
                {
                    Id = _id,
                    Name = _name,
                    Location = _location
                });
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullLocation", ex);
            }
        }

        [Then(@"devuelve el puerto editado en base de datos con los valores actualizados")]
        public void ThenDevuelveElPuertoEditadoEnBaseDeDatosConLosValoresActualizados()
        {
            Assert.AreEqual(_portUpdated.Name, _name);
            Assert.AreEqual(_portUpdated.Location, _location);
        }

        [Then(@"devuelve un error porque la localizacion del puerto es requerida")]
        public void ThenDevuelveUnErrorPorqueElNombreDeLaActividadEsRequerido()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullLocation");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
