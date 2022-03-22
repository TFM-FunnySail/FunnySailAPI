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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    public class CreateTechnicalServiceStep
    {
        private ScenarioContext _scenarioContext;
        private ITechnicalServiceCEN _serviceCEN;
        private ITechnicalServiceCAD _serviceCAD;
        private ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        private TechnicalServiceEN _newService;
        private decimal _price;
        private string _description;

        public CreateTechnicalServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _serviceCAD = new TechnicalServiceCAD(applicationDbContextFake._dbContextFake);
            _technicalServiceBoatCAD = new TechnicalServiceBoatCAD(applicationDbContextFake._dbContextFake);
            _serviceCEN = new TechnicalServiceCEN(_serviceCAD, _technicalServiceBoatCAD);

        }

        [Given(@"con precio y descripción (.*),(.*)")]
        public void GivenConPrecioYDescripcion(string price, string desc)
        {
            _description = desc;
            _price = Decimal.Parse(price);
        }

        [When(@"se adiciona el servicio técnico")]
        public async Task WhenSeAdicionaElServicioTecnico()
        {
            try
            {
                int id = await _serviceCEN.AddTechnicalService(_price, _description);
                _newService = await _serviceCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }

        [Then(@"devuelve el servicio técnico creado en base de datos con los mismos valores")]
        public void ThenDevuelveElServicioTecnicoCreadoEnBaseDeDatosConLosMismosValores()
        {
            Assert.AreEqual(_price, _newService.Price);
            Assert.AreEqual(_description, _newService.Description);
        }

        [Given(@"con precio (.*), sin descripcion")]
        public void GivenConPrecioSinDescripcion(string price)
        {
            _price = Decimal.Parse(price);
        }

        [Then(@"devuelve un error porque la descripcion del servicio técnico es requerida es requerida")]
        public void ThenDevuelveUnErrorPorqueLaDescripcionDelServicioTecnicoEsRequeridaEsRequerida()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }

    }
}
