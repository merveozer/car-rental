using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure.Persistence;
using Domain.Entities;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorTypesController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public ColorTypesController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/ColorTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorType>>> GetColorType()
        {
            return await _context.ColorType.ToListAsync();
        }

        // GET: api/ColorTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorType>> GetColorType(int id)
        {
            var colorType = await _context.ColorType.FindAsync(id);

            if (colorType == null)
            {
                return NotFound();
            }

            return colorType;
        }

        // PUT: api/ColorTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColorType(int id, ColorType colorType)
        {
            if (id != colorType.Id)
            {
                return BadRequest();
            }

            _context.Entry(colorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorTypeExists(id))
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

        // POST: api/ColorTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColorType>> PostColorType(ColorType colorType)
        {
            _context.ColorType.Add(colorType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColorType", new { id = colorType.Id }, colorType);
        }

        // DELETE: api/ColorTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColorType(int id)
        {
            var colorType = await _context.ColorType.FindAsync(id);
            if (colorType == null)
            {
                return NotFound();
            }

            _context.ColorType.Remove(colorType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorTypeExists(int id)
        {
            return _context.ColorType.Any(e => e.Id == id);
        }
    }
}
