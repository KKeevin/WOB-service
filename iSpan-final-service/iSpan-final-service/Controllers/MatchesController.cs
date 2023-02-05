using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using System.Globalization;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly WOBContext _context;

        public MatchesController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatch()
        {
            return await _context.Match.ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        //------------------------------------------------------------------------------
        // 【會員配對課程】
        //------------------------------------------------------------------------------
        [HttpPost("Filter")]
        public async Task<IEnumerable<MatchDTO>> FilterMatch([FromBody] MatchDTO match)
        {
            return _context.Match.Include(c => c.Calendar).Where(c => c.Calendar.OrdererId == match.OrdererId).Select(c => new MatchDTO
            {
                MatchId = c.MatchId,
                //ObjectId = c.Calendar.ObjectId,
                OrdererId = c.Calendar.OrdererId,
                ObjectName = _context.Member.Where(a => a.MemberId == c.Calendar.ObjectId).Select(name => name.Name).FirstOrDefault(),
                ObjectPic = _context.Member.Where(a => a.MemberId == c.Calendar.ObjectId).Select(pic => pic.Picture).FirstOrDefault(),
                Payment = c.Payment,
                IsPaid = c.IsPaid,
                IsConfirmed = c.Calendar.IsConfirmed,
                Content = c.Calendar.MyContent,
                OrderTime = c.OrderTime,
                DealTime = c.DealTime,
                StartDateTime = c.Calendar.StartDateTime,
            });
        }

        [HttpPost("Coach")]
        public async Task<IEnumerable<MatchForCoachDTO>> FilterMatchForCoach([FromBody] MatchDTO match)
        {

            return _context.Match.Include(c => c.Calendar).Where(c => c.Calendar.ObjectId == match.ObjectId).Select(c => new MatchForCoachDTO
            {
                MatchId = c.MatchId,
                ObjectId = c.Calendar.ObjectId,
                OrdererId = c.Calendar.OrdererId,
                Payment = c.Payment,
                IsPaid = c.IsPaid,
                IsConfirmed = c.Calendar.IsConfirmed,
                Content = c.Calendar.MyContent,
                OrderTime = c.OrderTime,
                DealTime = c.DealTime,
                StartDateTime = c.Calendar.StartDateTime,
            });
        }


        //[HttpPost("Coach")]
        //public async Task<IEnumerable<MatchForCoachDTO>> FilterMatchForCoach([FromBody] MatchDTO match)
        //{

        //    return _context.Match.Include(c => c.Calendar).Where(c => c.Calendar.ObjectId == match.ObjectId).Select(c => new MatchForCoachDTO
        //    {
        //        MatchId= c.MatchId,
        //        ObjectId = c.Calendar.ObjectId,
        //        OrdererId = c.Calendar.OrdererId,
        //        Payment = c.Payment,
        //        IsPaid = c.IsPaid,
        //        IsConfirmed = c.Calendar.IsConfirmed,
        //        Content = c.Calendar.MyContent,
        //        OrderTime = c.OrderTime,
        //        DealTime = c.DealTime,
        //        StartDateTime = c.Calendar.StartDateTime,
        //    });
        //}


        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Match.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.MatchId }, match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("MatchGym")]
        public async Task<string[]> MatchGym([FromBody] int[] match)
        {
            string[] gymname = new string[match.Count()];
            for (int i = 0; i < match.Count(); i++)
            {
                var temp= _context.GymOrder.Include(c => c.Order).Include(g => g.Gym).Where(go => go.Order.MatchId == match[i]).Select(c => c.Gym.GymName).FirstOrDefault();
                if(temp!=null)
                {
                    gymname[i] = (string)temp; 
                }
                else
                {
                    gymname[i] = "0";
                }
            }
            return gymname;
        }

        [HttpPost("AddMatch")]
        public async Task<Match[]> AddMatch([FromBody] AddMatchDTO[] myMatch)
        {

            Match[] calendars = new Match[myMatch.Count()];

            for (int i = 0; i < myMatch.Count(); i++)
            {
                Match add = new Match { };
                add.CalendarId = myMatch[i].CalendarId;
                add.Payment = myMatch[i].Payment;
                add.OrderTime = DateTime.Now;
                add.IsPaid = false;
                Console.WriteLine(myMatch[i]);
                calendars[i] = add;
            }
            await _context.Match.AddRangeAsync(calendars);
            await _context.SaveChangesAsync();
            return calendars;

        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchId == id);
        }
    }
}
