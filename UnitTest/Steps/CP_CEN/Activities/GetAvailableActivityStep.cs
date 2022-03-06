using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Features.CAD.Activitys
{
    [Binding]
    public class GetAvailableActivitysStep
    {
        private ScenarioContext _scenarioContext;
        private ApplicationDbContextFake _applicationDbContextFake;
        private IActivityCEN _activityCEN;
        private List<ActivityEN> _activities;
        private DateTime? _initialDate;
        private DateTime? _endDate;
        private Pagination _pagination;
        private Decimal? _minPrice;
        private Decimal? _maxPrice;
        private String _name;


        public GetAvailableActivitysStep(ScenarioContext scenarioContext)
        {
            _name = "";

            _scenarioContext = scenarioContext;

            _applicationDbContextFake = new ApplicationDbContextFake();

            IActivityCAD activityCAD = new ActivityCAD(_applicationDbContextFake._dbContextFake);
            _activityCEN = new ActivityCEN(activityCAD);

        }

        [Given(@"se piden las actividades disponibles con un precio minimo de (.*) y maximo de (.*)")]
        public void GivenSePidenLasActividadesDisponiblesConPreciosSuperioresA(decimal minPrice, decimal maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }

        [When(@"se obtienen las actividades disponibles con ese rango de precio")]
        public async Task WhenSeObtienenLasActividadesDisponiblesConEseRangoDePrecioAsync()
        {
            _activities = await _activityCEN.GetAvailableActivity(_pagination, _initialDate, _endDate, _minPrice, _maxPrice, _name);
        }

        [Then(@"el resultado debe ser una lista con los barcos con un precio mayor a (.*) y menor a (.*) que se encuentren activas")]
        public void ThenElResultadoDebeSerUnaListaConLosBarcosConUnPrecioMayorAQueSeEncuentrenActivas(decimal minPrice, decimal maxPrice)
        {
            int totalActiveInRange = _applicationDbContextFake._dbContextFake.Activity.Count(x => x.Price >= minPrice && x.Price < maxPrice && x.Active == true);
            Assert.IsTrue(!_activities.Any(x => x.Active == false));
            Assert.AreEqual(_activities.Count, totalActiveInRange);
        }

        [Given(@"se piden las actividades disponibles para las fechas (.*) y (.*)")]
        public void GivenSePidenLasActividadesDisponiblesParaLasFechasYAsync(string initialDate, string endDate)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }

        [When(@"se obtienen las actividades disponibles con esos rangos de fechas")]
        public async Task WhenSeObtienenLasActividadesDisponiblesAsync()
        {
            _activities = await _activityCEN.GetAvailableActivity(_pagination, _initialDate, _endDate, 0, 0, _name);
        }

        [Then(@"el resultado debe ser una lista con todas las actividades activas entre (.*) y (.*)")]
        public void ThenElResultadoDebeSerUnaListaConTodasLasActividadesActivasEntreYAsync(string initialDate, string endDate)
        {
            int totalActiveInRange = _applicationDbContextFake._dbContextFake.Activity.Count(x => x.ActivityDate >= DateTime.Parse(initialDate) && x.ActivityDate < DateTime.Parse(endDate) && x.Active == true);
            Assert.IsTrue(!_activities.Any(x => x.Active == false));
            Assert.AreEqual(_activities.Count, totalActiveInRange);
        }

        [Given(@"se piden las actividades disponibles con nombre (.*)")]
        public void GivenSePidenLasActividadesDisponiblesConNombre(string name)
        {
            _name = name;
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }

        [When(@"se obtienen las actividades disponibles")]
        public async Task WhenSeObtienenLasActividadesDisponibles()
        {
            _activities = await _activityCEN.GetAvailableActivity(_pagination, null, null, 0, 0, _name);
        }

        [Then(@"el resultado debe ser una lista con todas las actividades activas con nombre (.*)")]
        public void ThenElResultadoDebeSerUnaListaConTodasLasActividadesActivasConNombreAsync(string name)
        {

            int totalActiveWithName = _applicationDbContextFake._dbContextFake.Activity.Count(x => x.Name == name && x.Active == true);
            Assert.IsTrue(!_activities.Any(x => x.Active == false));
            Assert.AreEqual(_activities.Count, totalActiveWithName);
        }
    }
}
