using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymOrdersController : ControllerBase
    {
        private readonly WOBContext _context;

        public GymOrdersController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/GymOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymOrder>>> GetGymOrder()
        {
            return await _context.GymOrder.ToListAsync();
        }

        // GET: api/GymOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymOrder>> GetGymOrder(int id)
        {
            var gymOrder = await _context.GymOrder.FindAsync(id);

            if (gymOrder == null)
            {
                return NotFound();
            }

            return gymOrder;
        }

        // PUT: api/GymOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGymOrder(int id, GymOrder gymOrder)
        {
            if (id != gymOrder.GymOrderId)
            {
                return BadRequest();
            }

            _context.Entry(gymOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymOrderExists(id))
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
        */

        // POST: api/GymOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<GymOrder>> PostGymOrder(GymOrder gymOrder)
        {
            _context.GymOrder.Add(gymOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGymOrder", new { id = gymOrder.GymOrderId }, gymOrder);
        }

        // DELETE: api/GymOrders/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymOrder(int id)
        {
            var gymOrder = await _context.GymOrder.FindAsync(id);
            if (gymOrder == null)
            {
                return NotFound();
            }

            _context.GymOrder.Remove(gymOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        /*
        private bool GymOrderExists(int id)
        {
            return _context.GymOrder.Any(e => e.GymOrderId == id);
        }
        */
    }
}
