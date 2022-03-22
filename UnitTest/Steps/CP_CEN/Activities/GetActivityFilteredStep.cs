using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CP_CEN.Activities
{
    [Binding]
    public class GetActivityFilteredStep
    {

        private ScenarioContext _scenarioContext;
        private ApplicationDbContextFake _applicationDbContextFake;
        private IActivityCEN _activityCEN;
        private List<ActivityEN> _activities;
        private DateTime _initialDate;
        private DateTime _endDate;
        private Pagination _pagination;
        private Decimal _minPrice;
        private Decimal _maxPrice;
        private String _name;

        public GetActivityFilteredStep(ScenarioContext scenarioContext)
        {
            _name = "";

            _scenarioContext = scenarioContext;

            _applicationDbContextFake = new ApplicationDbContextFake();

            IActivityCAD activityCAD = new ActivityCAD(_applicationDbContextFake._dbContextFake);
            _activityCEN = new ActivityCEN(activityCAD);

        }

        [Given(@"se piden las actividades activas con un precio minimo de (.*) y maximo de (.*)")]
        public void GivenSePidenLasActividadesActivasConUnPrecioMinimoDeYMaximoDe(decimal minPrice, decimal maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }
        
        [Given(@"se piden las actividades activas con nombre (.*)")]
        public void GivenSePidenLasActividadesActivasConNombre(string name)
        {
            _name = name;
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }
        
        [When(@"se obtienen las actividades activas con ese rango de precio")]
        public async Task WhenSeObtienenLasActividadesActivasConEseRangoDePrecioAsync()
        {
            _activities = ((List<ActivityEN>)await _activityCEN.GetAll(pagination: new Pagination
            {
                Limit = 1000,
                Offset = 0
            }, filters: new ActivityFilters
            {
                MinPrice = _minPrice,
                MaxPrice = _maxPrice,
                Active = true
            }));
        }

        [Then(@"el resultado debe ser una lista con los barcos con un precio mayor a (.*) y menor a (.*) que se encuentren activas")]
        public void ThenElResultadoDebeSerUnaListaConLosBarcosConUnPrecioMayorAQueSeEncuentrenActivas(decimal minPrice, decimal maxPrice)
        {
            int totalActiveInRange = _applicationDbContextFake._dbContextFake.Activity.Count(x => x.Price >= minPrice && x.Price < maxPrice && x.Active == true);
            Assert.IsTrue(!_activities.Any(x => x.Active == false));
            Assert.AreEqual(_activities.Count, totalActiveInRange);
        }

        [When(@"se obtienen las actividades activas con ese nombre")]
        public async void WhenSeObtienenLasActividadesActivas()
        {
            _activities = ((List<ActivityEN>)await _activityCEN.GetAll(pagination: new Pagination
            {
                Limit = 1000,
                Offset = 0
            }, filters: new ActivityFilters
            {
                 Name = _name,
                 Active = true
            }));
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
