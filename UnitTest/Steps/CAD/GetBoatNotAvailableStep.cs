using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UnitTest.FakeFactories;

namespace UnitTest.Steps.CAD
{
    [Binding]
    public class GetBoatNotAvailableStep
    {
        private ScenarioContext _scenarioContext;
        private ApplicationDbContextFake _applicationDbContextFake;
        private IBoatCAD _boatCAD;
        private List<int> _boatsIds;
        private DateTime _initialDate;
        private DateTime _endDate;
        public GetBoatNotAvailableStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _applicationDbContextFake = new ApplicationDbContextFake();

            _boatCAD = new BoatCAD(_applicationDbContextFake._dbContextFake);
        }

        [Given(@"se piden los barcos reservados para las fechas (.*) y (.*) y no hay reserva con esa fecha")]
        public void GivenSePidenLosBarcosDisponiblesParaLasFechasYYNoHayReservaConEsaFecha(string initialDate, string endDate)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);

        }

        [When(@"se obtienen los barcos no disponibles")]
        public async Task WhenSeObtienenLosBarcosNoDisponibles()
        {
            _boatsIds = await _boatCAD.GetBoatIdsNotAvailable(_initialDate,_endDate);
        }

        [Then(@"el resultado debe ser una lista vacía")]
        public void ThenElResultadoDebeSerUnaListaVacia()
        {
            Assert.AreEqual(_boatsIds.Count, 0);
        }

        [Given(@"se piden los barcos reservados para las fechas (.*) y (.*) y los barcos (.*) y (.*) estan reservados para esas fechas")]
        public void GivenSePidenLosBarcosDisponiblesParaLasFechasYYLosBarcosYEstanReservadosParaEsasFechas(string initialDate, string endDate, int boatId1, int boatId2)
        {
            _initialDate = DateTime.Parse(initialDate);
            _endDate = DateTime.Parse(endDate);

            //Creando las reservas
             _applicationDbContextFake.Add<BookingEN>(new BookingEN
            {
                 ClientId = "1",
                 CreatedDate = DateTime.UtcNow,
                 EntryDate = _initialDate,
                 Paid = false,
                 DepartureDate = _endDate.AddHours(-1),
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

        [Then(@"el resultado debe ser una lista con los barcos (.*) y (.*)")]
        public void ThenElResultadoDebeSerUnaListaConLosBarcosY(int boatid1, int boatId2)
        {
            Assert.AreEqual(_boatsIds.Count, 2);
            Assert.IsTrue(_boatsIds.Contains(boatid1));
            Assert.IsTrue(_boatsIds.Contains(boatId2));
        }


    }
}
