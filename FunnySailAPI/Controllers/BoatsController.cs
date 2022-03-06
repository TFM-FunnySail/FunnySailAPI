using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;

namespace FunnySailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Boats
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<BoatOutputDTO>>> GetBoats(int? limit, int? offset)
        {
            try
            {
                var boatTotal = await _unitOfWork.BoatCEN.GetTotal();

                if (boatTotal == 0)
                    return NoContent();

                var pagination = new Pagination
                {
                    Limit = limit ?? 20,
                    Offset = offset ?? 0
                };

                var boats = (await _unitOfWork.BoatCEN.GetAll(pagination: pagination))
                    .Select(x=> BoatAssemblers.Convert(x));

                return new GenericResponseDTO<BoatOutputDTO>(boats,pagination.Limit,pagination.Offset,boatTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // GET: api/Boats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoatOutputDTO>> GetBoatEN(int id)
        {
            try
            {
                var boats = await _unitOfWork.BoatCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new BoatFilters
                {
                    BoatId = id
                });

                var boat = boats.Select(x => BoatAssemblers.Convert(x)).FirstOrDefault();
                if (boat == null)
                {
                    return NotFound();
                }

                return boat;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        //[HttpGet("availableBoats")]
        //public async Task<ActionResult<BoatEN>> GetBoatEN(int id)
        //{
        //    var boatEN = await _context.Boats.FindAsync(id);

        //    if (boatEN == null)
        //    {
        //        return NotFound();
        //    }

        //    return boatEN;
        //}

        //// PUT: api/Boats/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBoatEN(int id, BoatEN boatEN)
        //{
        //    try
        //    {
        //        if (id != boatEN.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(boatEN).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BoatENExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }
        //    catch (DataValidationException ex)
        //    {

        //        throw;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //// POST: api/Boats
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<BoatEN>> PostBoatEN(BoatEN boatEN)
        //{
        //    _context.Boats.Add(boatEN);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBoatEN", new { id = boatEN.Id }, boatEN);
        //}

        //// DELETE: api/Boats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<BoatEN>> DeleteBoatEN(int id)
        //{
        //    var boatEN = await _context.Boats.FindAsync(id);
        //    if (boatEN == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Boats.Remove(boatEN);
        //    await _context.SaveChangesAsync();

        //    return boatEN;
        //}

    }
}
