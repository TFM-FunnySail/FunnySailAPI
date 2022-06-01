using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.FakeFactories;

namespace UnitTest.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        private BookingController _BookingController;
        private IUnitOfWork _UnitOfWork;
        private UnitOfWorkFake _UnitOfWorkFake;
       
        public BookingControllerTest() 
        {
            _UnitOfWorkFake = new UnitOfWorkFake();
            _UnitOfWork = _UnitOfWorkFake.unitOfWork;
            _BookingController = new BookingController(_UnitOfWork);
        }

        [TestMethod]
        public void GetBookings_ShouldReturnAllBookings()
        {
            var bookings = _BookingController.GetBookings(new BookingFilters { RequestCaptain = true }, new Pagination() );
            Assert.IsNotNull(bookings);
            Assert.AreEqual(1, bookings.Result.Value.Total);
        }

        [TestMethod]
        public void GetBooking_ShouldReturnOneBooking()
        {
            var bookings = _BookingController.GetBooking(1);
            Assert.IsNotNull(bookings);
            Assert.AreEqual(1, bookings.Result.Value.Id);
        }

        [TestMethod]
        public void GetBooking_ShouldReturnNotFound()
        {
            var bookings = _BookingController.GetBooking(2);
            Assert.IsNotNull(bookings);
            Assert.AreEqual(404, new NotFoundObjectResult(bookings.Result.Result).StatusCode);
        }

        [TestMethod]
        public void CancelBooking_ShouldReturnNotContent()
        {
            var bookings = _BookingController.CancelBooking(1);
            Assert.IsNotNull(bookings);
            Assert.IsInstanceOfType(bookings.Result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void PayBooking_ShouldReturnNotContent()
        {
            var booking = _BookingController.PayBooking(1);
            Assert.IsNotNull(booking);
            Assert.IsInstanceOfType(booking.Result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void CreateBooking_ShouldRetrunId()
        {
            var booking = _BookingController.CreateBooking(new AddBookingInputDTO {
                ClientId = "1",
                TotalPeople = 10,
                RequestCaptain = true,
                Boats = new List<AddBoatBookingInputDTO> { new AddBoatBookingInputDTO { 
                    BoatId = 1,
                    DepartureDate = DateTime.UtcNow,
                    EntryDate = DateTime.UtcNow,
                },new AddBoatBookingInputDTO {
                    BoatId = 2,
                    DepartureDate = DateTime.UtcNow,
                    EntryDate = DateTime.UtcNow,
                },new AddBoatBookingInputDTO {
                    BoatId = 3,
                    DepartureDate = DateTime.UtcNow,
                    EntryDate = DateTime.UtcNow,
                } }
            });

            Assert.IsNotNull(booking); 
        }

        [TestMethod]
        public void UpdateBooking_ShouldReturnStatus200()
        {
            var booking = _BookingController.UpdateBooking(1, new UpdateBookingInputDTO
            {
                Id = 1,
                TotalPeople = 5
            });

            Assert.IsNotNull(booking);
            Assert.AreEqual(200, new OkObjectResult(booking.Result.Result).StatusCode);
        }

        [TestMethod]
        public void GetBookingStatus_ShouldReturnStatus200()
        {
            var booking = _BookingController.GetBookingsStatus();
            Assert.IsNotNull(booking);
            Assert.AreEqual(200, new OkObjectResult(booking.Result).StatusCode);
        }
    }
}
