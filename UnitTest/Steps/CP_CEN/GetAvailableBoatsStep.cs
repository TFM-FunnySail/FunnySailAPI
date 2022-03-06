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

namespace UnitTest.Steps.CP_CEN
{
    [Binding]
    public class GetAvailableBoatsStep
    {
        private ScenarioContext _scenarioContext;
        private ApplicationDbContextFake _applicationDbContextFake;
        private IBoatCEN _boatCEN;
        private List<BoatEN> _boats;
        private DateTime _initialDate;
        private DateTime _endDate;
        private Pagination _pagination;
        public GetAvailableBoatsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _applicationDbContextFake = new ApplicationDbContextFake();

            IBoatCAD boatCAD = new BoatCAD(_applicationDbContextFake._dbContextFake);
            _boatCEN = new BoatCEN(boatCAD);

        }

        [Given(@"se piden los barcos disponibles para las fechas (.*) y (.*),no hay reserva con esa fecha")]
        public void GivenSePidenLosBarcosDisponiblesParaLasFechasYNoHayReservaConEsaFecha(string initialDate, string endDate)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
        }

        [When(@"se obtienen los barcos disponibles")]
        public async Task WhenSeObtienenLosBarcosDisponibles()
        {
            _boats = (await _boatCEN.GetAvailableBoats(_pagination,_initialDate,_endDate)).ToList();
        }

        [Then(@"el resultado debe ser una lista con todos los barcos activos")]
        public void ThenElResultadoDebeSerUnaListaConTodosLosBarcosActivos()
        {
            int totalActive = _applicationDbContextFake._dbContextFake.Boats.Count(x => x.Active);
            Assert.IsTrue(!_boats.Any(x=>x.Active == false));
            Assert.AreEqual(_boats.Count, totalActive);
        }

        [Given(@"se piden los barcos disponibles para las fechas (.*) y (.*) y los barcos (.*) y (.*) estan reservados para esas fechas")]
        public void GivenSePidenLosBarcosDisponiblesParaLasFechasYYLosBarcosYEstanReservadosParaEsasFechas(string initialDate, string endDate, int boatId1, int boatId2)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1000
            };
            //Creando las reservas
            _applicationDbContextFake.Add<BookingEN>(new BookingEN
            {
                ClientId = "1",
                CreatedDate = DateTime.UtcNow,
                EntryDate = _initialDate,
                Paid = false,
                DepartureDate = _endDate.AddHours(-1),
                RequestCaptain = true,
                Status = BookingStatusEnum.Booking,
                TotalPeople = 2,
                BoatBookings = new List<BoatBookingEN>
                    {
                        new BoatBookingEN
                        {
                            BoatId = boatId1,
                            Price = 10
                        },
                        new BoatBookingEN
                        {
                            BoatId = boatId2,
                            Price = 12
                        }
                    }
            });
        }

        [Then(@"el resultado debe ser una lista sin los barcos (.*) y (.*)")]
        public void ThenElResultadoDebeSerUnaListaSinLosBarcosY(int boatId1, int boatId2)
        {
            Assert.IsTrue(!_boats.Any(x=>x.Id == boatId1 || x.Id == boatId2));
        }

        [Given(@"se piden los barcos disponibles para las fechas (.*) y (.*),no hay reserva con esa fecha y se pide un solo barco")]
        public void GivenSePidenLosBarcosDisponiblesParaLasFechasYNoHayReservaConEsaFechaYSePideUnSoloBarco(string initialDate, string endDate)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);
            _pagination = new Pagination
            {
                Offset = 0,
                Limit = 1
            };
        }

        [Then(@"el resultado debe ser una lista con solo un barco")]
        public void ThenElResultadoDebeSerUnaListaConSoloUnBarco()
        {
            Assert.AreEqual(_boats.Count, 1);
        }

    }
}
