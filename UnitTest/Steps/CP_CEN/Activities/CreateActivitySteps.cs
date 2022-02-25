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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;

namespace UnitTest.Steps.CP_CEN.Activities
{
    [Binding]
    public class CreateActivityStep
    {
        private ScenarioContext _scenarioContext;
        private IActivityCEN _activityCEN;
        private IActivityCAD _activityCAD;
        private IActivityBookingCAD _activityBookingCAD;
        private ActivityEN _newActivity;
        private decimal _price;
        private string _description;
        private string _name;
        private DateTime _activityDate;

        public CreateActivityStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var applicationDbContextFake = new ApplicationDbContextFake();
            _activityCAD = new ActivityCAD(applicationDbContextFake._dbContextFake);
            _activityBookingCAD = new ActivityBookingCAD(applicationDbContextFake._dbContextFake);
            _activityCEN = new ActivityCEN(_activityCAD);

        }

        [Given(@"con (.*), (.*),(.*) y (.*)")]
        public void GivenConNombrePrecioYDescripcion(string name, string price, string desc, DateTime activityDate)
        {
            _name = name;
            _description = desc;
            _price = Decimal.Parse(price);
            _activityDate = activityDate;
        }
        
        [Given(@"con precio (.*), (.*), (.*) y sin nombre")]
        public void GivenConPrecioYSinNombre(string price, string desc, DateTime activityDate)
        {
            _description = desc;
            _price = Decimal.Parse(price);
            _activityDate = activityDate;
        }
        
        [When(@"se adiciona la actividad")]
        public async Task WhenSeAdicionaLaActividadAsync()
        {
            try
            {
                int id = await _activityCEN.AddActivity(new AddActivityInputDTO
                {
                    Active = true,
                    Price = _price,
                    Description = _description,
                    Name = _name,
                    ActivityDate = _activityDate
                });
                _newActivity = await _activityCAD.FindById(id);
            }
            catch (DataValidationException ex)
            {
                _scenarioContext.Add("Exception_NullName", ex);
            }
        }
        
        [Then(@"devuelve la actividad creada en base de datos con los mismos valores")]
        public void ThenDevuelveLaActividadCreadaEnBaseDeDatosConLosMismosValores()
        {
            Assert.AreEqual(_price, _newActivity.Price);
            Assert.AreEqual(_description, _newActivity.Description);
            Assert.AreEqual(_name, _newActivity.Name);
            Assert.AreEqual(_activityDate, _newActivity.ActivityDate);
        }
        
        [Then(@"devuelve un error porque el nombre de la actividad es requerida")]
        public void ThenDevuelveUnErrorPorqueElNombreDeLaActividadEsRequerida()
        {
            DataValidationException ex = _scenarioContext.Get<DataValidationException>("Exception_NullName");
            Assert.IsNotNull(ex);
            Assert.AreEqual(ExceptionTypesEnum.IsRequired, ex.ExceptionType);
        }
    }
}
