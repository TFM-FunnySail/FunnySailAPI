using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Helpers;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Booking;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
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
    [CustomAuthorize]
    [ApiController]
    public class BookingController : BaseController
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
                                        .Include(x => x.InvoiceLine)
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
                                        .ThenInclude(x=>x.ApplicationUser)
                                        .Include(x => x.ActivityBookings)
                                        .ThenInclude(x=>x.Activity)
                                        .Include(x => x.BoatBookings)
                                        .ThenInclude(x => x.Boat.BoatInfo)
                                        .Include(x=>x.BoatBookings)
                                        .ThenInclude(x=>x.Boat.BoatResources)
                                        .ThenInclude(x=>x.Resource)
                                        .Include(x => x.ServiceBookings)
                                        .ThenInclude(x=>x.service)
                                        .Include(x=>x.InvoiceLine)
                                        .Include(x=>x.Refunds));

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

        // Put: api/Bookings/5
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<BookingOutputDTO>> CancelBooking(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.BookingCP.CancelBooking(id);

                return NoContent();
            }
            catch (DataValidationException dataValidation)
            {
                if (dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // Put: api/Bookings/5
        [HttpPut("{id}/pay")]
        public async Task<ActionResult<BookingOutputDTO>> PayBooking(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                await _unitOfWork.BookingCP.PayBooking(id);
                return NoContent();
            }
            catch (DataValidationException dataValidation)
            {
                if (dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // Post: api/Bookings
        [HttpPost]
        public async Task<ActionResult<BookingOutputDTO>> CreateBooking(AddBookingInputDTO bookingInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                bookingInput.ClientId = User.UserId;

                int bookingId = await _unitOfWork.BookingCP.CreateBooking(bookingInput);
                return CreatedAtAction("GetBoat", new { id = bookingId });
            }
            catch (DataValidationException dataValidation)
            {
                if (dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // Put: api/Bookings/5
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<ActionResult<BookingOutputDTO>> UpdateBooking(int id,UpdateBookingInputDTO updateBookingInputDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                updateBookingInputDTO.Id = id;

                BookingEN booking = await _unitOfWork.BookingCP.UpdateBooking(updateBookingInputDTO);
                return Ok(BookingAssemblers.Convert(booking));
            }
            catch (DataValidationException dataValidation)
            {
                if (dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Bookings/status
        [HttpGet("status")]
        public ActionResult<IList<EnumsOutputDTO>> GetBookingsStatus()
        {
            try
            {
                return Ok(EnumsAssemblers.Convert<BookingStatusEnum>());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

    }
}
