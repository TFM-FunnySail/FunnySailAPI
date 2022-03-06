﻿using System;
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
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.Globals;

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
        public async Task<ActionResult<GenericResponseDTO<BoatOutputDTO>>> GetBoats([FromQuery] BoatFilters filters,[FromQuery] Pagination pagination)
        {
            try
            {
                var boatTotal = await _unitOfWork.BoatCEN.GetTotal(filters);

                var boats = (await _unitOfWork.BoatCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination(),
                    includeProperties: source=>source.Include(x=>x.BoatInfo)
                                        .Include(x => x.BoatPrices)
                                        .Include(x=>x.RequiredBoatTitles)
                                        .Include(x => x.Mooring)
                                        .ThenInclude(x => x.Port)
                                        .Include(x => x.BoatResources)
                                        .ThenInclude(x=>x.Resource)
                     ))
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
        public async Task<ActionResult<BoatOutputDTO>> GetBoat(int id)
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
                }, includeProperties: source => source.Include(x => x.BoatInfo)
                                         .Include(x => x.BoatPrices)
                                         .Include(x => x.RequiredBoatTitles)
                                         .Include(x => x.Mooring)
                                         .ThenInclude(x => x.Port)
                                         .Include(x => x.BoatResources)
                                         .ThenInclude(x => x.Resource));

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

        // GET: api/Boats/availableBoats
        [HttpGet("availableBoats")]
        public async Task<ActionResult<GenericResponseDTO<BoatOutputDTO>>> GetAvailableBoats(DateTime initialDate,DateTime endDate,[FromQuery]Pagination pagination)
        {
            try
            {
                var boatTotal = await _unitOfWork.BoatCEN.GetTotal();

                var boats = (await _unitOfWork.BoatCEN.GetAvailableBoats(pagination: pagination ?? new Pagination(),
                    initialDate: initialDate, 
                    endDate: endDate,
                    includeProperties: source => source.Include(x => x.BoatInfo)
                                        .Include(x => x.BoatPrices)
                                        .Include(x => x.RequiredBoatTitles)
                                        .Include(x => x.Mooring)
                                        .ThenInclude(x => x.Port)
                                        .Include(x => x.BoatResources)
                                        .ThenInclude(x => x.Resource)
                     ))
                    .Select(x => BoatAssemblers.Convert(x));

                return new GenericResponseDTO<BoatOutputDTO>(boats, pagination.Limit, pagination.Offset, boatTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/Boats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoatEN(int id, UpdateBoatInputDTO updateBoatInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateBoatInput.BoatId)
                    return BadRequest();

                await _unitOfWork.BoatCP.UpdateBoat(updateBoatInput);

                return NoContent();
            }
            catch (DataValidationException dataValidation)
            {
                if(dataValidation.ExceptionType == ExceptionTypesEnum.NotFound)
                    return NotFound();

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // POST: api/Boats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BoatEN>> PostBoat(AddBoatInputDTO boatInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int boatId = await _unitOfWork.BoatCP.CreateBoat(boatInput);

                return CreatedAtAction("GetBoat", new { id = boatId });
            }
            catch (DataValidationException dataValidation)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorResponseDTO(dataValidation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
            
        }

        // PUT: api/Boats/5/approve 
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> PutApproveBoat(int id)
        {
            try
            {
                await _unitOfWork.BoatCEN.ApproveBoat(id);

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

        // PUT: api/Boats/5/disapprove 
        [HttpPut("{id}/disapprove")]
        public async Task<IActionResult> PutDisapproveBoat(int boatId,DisapproveBoatInputDTO disapproveBoatInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                //Obtengo el id del admin por el token
                //disapproveBoatInput.AdminId = 

                await _unitOfWork.BoatCP.DisapproveBoat(boatId,disapproveBoatInput);

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

    }
}
