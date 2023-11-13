using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EWeatherAPI.Models;

namespace EWeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationMeasurementsController : ControllerBase
    {
        private readonly StationMeasurementContext _context;

        public StationMeasurementsController(StationMeasurementContext context)
        {
            _context = context;
        }

        // GET: api/StationMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationMeasurement>>> GetStationMeasurements()
        {
          if (_context.StationMeasurements == null)
          {
              return NotFound();
          }
            return await _context.StationMeasurements.ToListAsync();
        }

        // GET: api/StationMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationMeasurement>> GetStationMeasurement(long id)
        {
          if (_context.StationMeasurements == null)
          {
              return NotFound();
          }
            var stationMeasurement = await _context.StationMeasurements.FindAsync(id);

            if (stationMeasurement == null)
            {
                return NotFound();
            }

            return stationMeasurement;
        }

        // PUT: api/StationMeasurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationMeasurement(long id, StationMeasurement stationMeasurement)
        {
            if (id != stationMeasurement.Id)
            {
                return BadRequest();
            }

            _context.Entry(stationMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationMeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StationMeasurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StationMeasurement>> PostStationMeasurement(StationMeasurement stationMeasurement)
        {
          if (_context.StationMeasurements == null)
          {
              return Problem("Entity set 'StationMeasurementContext.StationMeasurements'  is null.");
          }
            _context.StationMeasurements.Add(stationMeasurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationMeasurement", new { id = stationMeasurement.Id }, stationMeasurement);
        }

        // DELETE: api/StationMeasurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationMeasurement(long id)
        {
            if (_context.StationMeasurements == null)
            {
                return NotFound();
            }
            var stationMeasurement = await _context.StationMeasurements.FindAsync(id);
            if (stationMeasurement == null)
            {
                return NotFound();
            }

            _context.StationMeasurements.Remove(stationMeasurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationMeasurementExists(long id)
        {
            return (_context.StationMeasurements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
