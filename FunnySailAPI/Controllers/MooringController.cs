using FunnySailAPI.ApplicationCore.Constants;
using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using FunnySailAPI.Assemblers;
using FunnySailAPI.DTO.Output;
using FunnySailAPI.DTO.Output.Mooring;
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
    [ApiController]
    public class MooringController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestUtilityService _requestUtilityService;

        public MooringController(IUnitOfWork unitOfWork,
                               IRequestUtilityService requestUtilityService)
        {
            _unitOfWork = unitOfWork;
            _requestUtilityService = requestUtilityService;
        }

        //GET: api/Mooring
        [HttpGet]
        public async Task<ActionResult<GenericResponseDTO<MooringOutputDTO>>> GetMoorings([FromQuery] MooringFilters filters, [FromQuery] Pagination pagination)
        {
            try
            {
                int mooringTotal = await _unitOfWork.MooringCEN.GetTodos(filters);

                var moorings = (await _unitOfWork.MooringCEN.GetAll(
                    filters: filters,
                    pagination: pagination ?? new Pagination()
                    ,
                    includeProperties: source => source.Include(x => x.Port)

                     ))
                    .Select(x => MooringAssemblers.Convert(x));

                return new GenericResponseDTO<MooringOutputDTO>(moorings, pagination.Limit, pagination.Offset, mooringTotal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        //GET: api/mooring/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MooringOutputDTO>> GetMooring(int id)
        {
            try
            {
                var moorings = await _unitOfWork.MooringCEN.GetAll(pagination: new Pagination
                {
                    Limit = 1,
                    Offset = 0
                }, filters: new MooringFilters
                {
                    MooringId = id
                });

                var mooring = moorings.Select(x => MooringAssemblers.Convert(x)).FirstOrDefault();
                if (mooring == null)
                {
                    return NotFound();
                }

                return mooring;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO(ex));
            }
        }

        // PUT: api/mooring/5
        [CustomAuthorize(UserRolesConstant.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMooringEN(int id, UpdateMooringDTO updateMooring)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (id != updateMooring.MooringId)
                    return BadRequest();

                await _unitOfWork.MooringCEN.UpdateMooring(updateMooring);

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

        // POST: api/Mooring
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult<MooringEN>> PostMooring(AddMooringDTO mooringInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                int mooringId = await _unitOfWork.PortMooringCP.AddMooring(mooringInput.PortId, mooringInput.Alias, mooringInput.Type);

                return CreatedAtAction("GetMooring", new { id = mooringId });
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

        //    public class MooringController : Controller
        //{
        //    // GET: MooringController
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }

        //    // GET: MooringController/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        return View();
        //    }

        //    // GET: MooringController/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: MooringController/Create
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: MooringController/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: MooringController/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: MooringController/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: MooringController/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}
