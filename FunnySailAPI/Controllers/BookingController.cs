using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.DTO.Output.Booking;
using FunnySailAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [CustomAuthorize(UserRolesConstant.ADMIN)]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<BookingOutputDTO>>> GetBookings([FromQuery] BookingFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                var total = await _unitOfWork.BookingCEN.GetTotal(filters);

                var itemResults = (await _unitOfWork.BookingCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source => source.Include(x => x.Client)
                                        .Include(x => x.ActivityBookings)
                                        .Include(x => x.BoatBookings)
                                        .Include(x => x.ServiceBookings)
                     ))
                    .Select(x => BookingAssemblers.Convert(x));

                return new GenericResponseDTO<BookingOutputDTO>(itemResults, pagination.Limit, pagination.Offset, total);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingOutputDTO>> GetBooking(int id)
        {
            try
            {
                var itemResult = await _unitOfWork.BookingCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new BookingFilters
                {
                    bookingId = id
                }, includeProperties: source => source.Include(x => x.Client)
                                        .Include(x => x.ActivityBookings)
                                        .Include(x => x.BoatBookings)
                                        .Include(x => x.ServiceBookings));

                var booking = itemResult.Select(x => BookingAssemblers.Convert(x)).FirstOrDefault();
                if (booking == null)
                {
                    return NotFound();
                }

                return booking;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

    }
}
