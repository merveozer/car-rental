using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure.Persistence;
using Domain.Entities;

namespace WebApp.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentVehiclesController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public RentVehiclesController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/RentVehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentVehicle>>> GetRentVehicle()
        {
            return await _context.RentVehicle.ToListAsync();
        }

        // GET: api/RentVehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentVehicle>> GetRentVehicle(int id)
        {
            var rentVehicle = await _context.RentVehicle.FindAsync(id);

            if (rentVehicle == null)
            {
                return NotFound();
            }

            return rentVehicle;
        }

        // PUT: api/RentVehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentVehicle(int id, RentVehicle rentVehicle)
        {
            if (id != rentVehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentVehicleExists(id))
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

        // POST: api/RentVehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RentVehicle>> PostRentVehicle(RentVehicle rentVehicle)
        {
            _context.RentVehicle.Add(rentVehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentVehicle", new { id = rentVehicle.Id }, rentVehicle);
        }

        // DELETE: api/RentVehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentVehicle(int id)
        {
            var rentVehicle = await _context.RentVehicle.FindAsync(id);
            if (rentVehicle == null)
            {
                return NotFound();
            }

            _context.RentVehicle.Remove(rentVehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentVehicleExists(int id)
        {
            return _context.RentVehicle.Any(e => e.Id == id);
        }
    }
}
