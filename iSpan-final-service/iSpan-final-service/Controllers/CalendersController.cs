using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using NuGet.Packaging;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly WOBContext _context;

        public CalendarsController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendar()
        {
            return await _context.Calendar.ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(int id)
        {
            var Calendar = await _context.Calendar.FindAsync(id);

            if (Calendar == null)
            {
                return NotFound();
            }

            return Calendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(int id, Calendar Calendar)
        {
            if (id != Calendar.CalendarId)
            {
                return BadRequest();
            }
            
            _context.Entry(Calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
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

        // POST: api/Calendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(Calendar Calendar)
        {
            _context.Calendar.Add(Calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendar", new { id = Calendar.CalendarId }, Calendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var Calendar = await _context.Calendar.FindAsync(id);
            if (Calendar == null)
            {
                return NotFound();
            }

            _context.Calendar.Remove(Calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Filter")]
        public async Task<IEnumerable<CalendarDTO>> FilterCalendarByObject([FromBody] CalendarDTO Calendar)
        {
            return _context.Calendar.Where(
                cal => cal.ObjectId == Calendar.ObjectId).Select(cal => new CalendarDTO
                {
                    CalendarId = cal.CalendarId,
                    ObjectId= cal.ObjectId,
                    OrdererId=cal.OrdererId,
                    Start = cal.StartDateTime,
                    IsConfirmed = cal.IsConfirmed,
                });
        }

        [HttpPost("Add")]
        public async Task<Calendar[]> SendCalendarByObject([FromBody] CalendarDTO[] myCalendar)
        {
            Calendar[] calendars = new Calendar[myCalendar.Count()];

            for (int i = 0; i < myCalendar.Count(); i++)
            {
                Calendar addcalendar = new Calendar { };
                addcalendar.ObjectId = myCalendar[i].ObjectId;
                addcalendar.OrdererId = null;
                addcalendar.StartDateTime = myCalendar[i].Start;
                Console.WriteLine(myCalendar[i]);
                calendars[i] = addcalendar;
            }
            await _context.Calendar.AddRangeAsync(calendars);
            await _context.SaveChangesAsync();
            return calendars;

        }

        [HttpPatch("Book")]
        public async Task<CalendarDTO[]> BookCalendarByObject([FromBody] CalendarDTO[] myCalendar)
        {

            for (int i = 0; i < myCalendar.Count(); i++)
            {
                Calendar addcalendar = new Calendar { };
                addcalendar.ObjectId = myCalendar[i].ObjectId;
                addcalendar.OrdererId = myCalendar[i].OrdererId;
                addcalendar.StartDateTime = myCalendar[i].Start;
                addcalendar.CalendarId = myCalendar[i].CalendarId;
                addcalendar.IsConfirmed = false;
                addcalendar.OrderDate = DateTime.Now;
                _context.Entry(addcalendar).State = EntityState.Modified;
                Console.WriteLine(addcalendar);
            }
            await _context.SaveChangesAsync();

            return myCalendar;

        }

        private bool CalendarExists(int id)
        {
            return _context.Calendar.Any(e => e.CalendarId == id);
        }
    }
}
