using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Boat
{
    [Binding]
    class AddTechnicalServiceBoatStep
    {
        private ScenarioContext _scenarioContext;
        private ApplicationDbContext _dbContextFake;
        private ITechnicalServiceCEN _technicalserviceCEN;
        private ITechnicalServiceCAD _technicalserviceCAD;
        private ITechnicalServiceBoatCAD _technicalserviceBoatCAD;
        private ITechnicalServiceCP _technicalServiceCP;
        private IBoatCEN _boatCEN;
        private IBoatCAD _boatCAD;
        private int _boatId;
        private int _technicalServiceId;
        private DateTime _serviceDate;
        private decimal _price;
        private int _id;
        public AddTechnicalServiceBoatStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _technicalserviceBoatCAD = new TechnicalServiceBoatCAD(applicationDbContextFake._dbContextFake);
            _technicalserviceCAD = new TechnicalServiceCAD(applicationDbContextFake._dbContextFake);
            _boatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(_boatCAD);
            _technicalserviceCEN = new TechnicalServiceCEN(_technicalserviceCAD, _technicalserviceBoatCAD);
            _technicalServiceCP = new TechnicalServiceCP(_technicalserviceCEN, _boatCEN);
        }

        [Given(@"una programación de un servicio técnico para un barco")]
        public void GivenUnaProgramacionDeUnServicioTecnicoParaUnBarco()
        {
            _boatId = 1;
            _technicalServiceId = 1;
            _serviceDate = DateTime.Now;
            _price = (decimal)100;
        }

        [When(@"se invoca a la función para añadirlo a la agenda")]
        public async void WhenSeInvocaALaFuncionParaAnadirloALaAgenda()
        {
            try
            {
                _id = await _technicalServiceCP.ScheduleTechnicalServiceToBoat(new ScheduleTechnicalServiceDTO
                {
                    BoatId = _boatId,
                    TechnicalServiceId = _technicalServiceId,
                    ServiceDate = _serviceDate,
                    Price = _price
                });
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }

        [Then(@"se agenda la programación y se devuelve el id")]
        public async void ThenSeAgendaLaProgramacionYSeDevuelveElId()
        {
            TechnicalServiceBoatEN technicalServiceBoatEN = await _technicalserviceBoatCAD.FindById(_id);
            Assert.AreEqual(_boatId, technicalServiceBoatEN.BoatId);
            Assert.AreEqual(_technicalServiceId, technicalServiceBoatEN.TechnicalServiceId);
            Assert.AreEqual(_serviceDate, technicalServiceBoatEN.ServiceDate);
            Assert.AreEqual(_price, technicalServiceBoatEN.Price);
        }

        [Given(@"una programación de un servicio técnico para un barco no disponible")]
        public void GivenUnaProgramacionDeUnServicioTecnicoParaUnBarcoNoDisponible()
        {
            _boatId = 1;
            _technicalServiceId = 1;
            _serviceDate = DateTime.Now.AddDays(10);
            _price = (decimal)100;
        }

        [Then(@"se devuelve un error de servicio ocupado")]
        public void ThenSeDevuelveUnErrorDeServicioOcupado()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>
               ("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Technical support is not available for the requested date", ex.EnMessage);
        }

        [Given(@"una programación de un servicio técnico para un barco con datos incompletos")]
        public void GivenUnaProgramacionDeUnServicioTecnicoParaUnBarcoConDatosIncompletos()
        {
            _boatId = 1;
            _technicalServiceId = -1;
            _serviceDate = DateTime.Now;
            _price = (decimal)100;
        }

        [When(@"se invoca a la función para añadirlo la agenda")]
        public async void WhenSeInvocaALaFuncionParaAnadirloLaAgenda()
        {
            try
            {
                _id = await _technicalServiceCP.ScheduleTechnicalServiceToBoat(new ScheduleTechnicalServiceDTO
                {
                    BoatId = _boatId,
                    TechnicalServiceId = 110,
                    ServiceDate = _serviceDate,
                    Price = _price
                });
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Ex_NotFound", ex);
            }
        }


        [Then(@"se devuelve un error de que no se encuentra el servicio que se quiere programar")]
        public void ThenSeDevuelveUnErrorDeQueNoSeEncuentraElServicioQueSeQuiereProgramar()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Ex_NotFound");

            Assert.IsNotNull(ex);
            Assert.AreEqual("Technical service not found.", ex.EnMessage);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

    }
}
