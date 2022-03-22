using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Services;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.TechnicalService
{
    [Binding]
    class UpdatedTechnicalServiceStep
    {

        private ScenarioContext _scenarioContext;
        private ITechnicalServiceCEN _serviceCEN;
        private ITechnicalServiceCAD _serviceCAD;
        private ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        private decimal _price;
        private string _description;
        private int _id;
        private bool _active; 
        private TechnicalServiceEN _technicalService;
        public UpdatedTechnicalServiceStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _serviceCAD = new TechnicalServiceCAD(applicationDbContextFake._dbContextFake);
            _technicalServiceBoatCAD = new TechnicalServiceBoatCAD(applicationDbContextFake._dbContextFake);
            _serviceCEN = new TechnicalServiceCEN(_serviceCAD, _technicalServiceBoatCAD);
        }

        [Given(@"Dado un id que no exista")]
        public void GivenDadoUnIdQueNoExista()
        {
            _id = 0;
            _description = "newDescription";
            _price = (decimal)5.0;
            _active = true;
        }

        [When(@"Se modifique el servicio tecnico")]
        public async void WhenSeModifiqueElServicioTecnico()
        {
            try
            {
                _technicalService =  await _serviceCEN.UpdateTechnicalService(new UpdateTechnicalServiceDTO
                {
                    Id = _id,
                    Description = _description,
                    Price = _price,
                    Active = _active
                });
            }
            catch(Exception ex) 
            {
                _scenarioContext.Add("Exception_NullDesc", ex);
            }
        }

        [Then(@"Dara una excepcion y no se actualizara")]
        public void ThenDaraUnaExcepcionYNoSeActualizara()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullDesc");

            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.NotFound, ex.ExceptionType);
        }

        [Given(@"Dados un id correcto y los datos correctos")]
        public void GivenDadosUnIdCorrectoYLosDatosCorrectos()
        {
            _id = 1;
            _description = "newDescription";
            _price = (decimal)5.0;
            _active = true;
        }

        [Then(@"Se actualizarán los datos correctamente\.")]
        public void ThenSeActualizaranLosDatosCorrectamente_()
        {
            Assert.AreEqual(_id, _technicalService.Id);
            Assert.AreEqual(_active, _technicalService.Active);
            Assert.AreEqual(_price, _technicalService.Price);
            Assert.AreEqual(_description, _technicalService.Description);
        }

    }
}
