using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using NuGet.Protocol;
using Microsoft.Win32;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymsController : ControllerBase
    {
        private readonly WOBContext _context;

        public GymsController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Gyms
        //[HttpGet]
        //public async Task<IEnumerable<GymListDTO>> GetGym()
        //{
        //    var gym = _context.Gym.Include(a => a.Area).Select(c => new GymListDTO
        //    {
        //        GymId = c.GymId,
        //        GymName = c.GymName,
        //        Area = c.Area.Village,
        //        Web = c.Web,
        //        ImgPath = c.ImgPath,
        //        Phone = c.Phone,
        //    });
        //    return gym;
        //}

        [HttpPost("Login")]
        public async Task<GymLoginDTO> LoginGym([FromBody] GymLoginDTO gym)
        {

            var aa = _context.Gym;
            var aaa = _context.Gym.Where(login => CheckLoginInfo(gym, login));
            return await _context.Gym.Where(login => login.Account.Contains(gym.Account) && login.Password == gym.Password).Select(login => new GymLoginDTO
            {
                Account = login.Account,
                GymId = login.GymId,
            }).FirstAsync();
        }
        private static bool CheckLoginInfo(GymLoginDTO member, Gym login)
        {
            //return login.Email.Contains(member.Email) && login.Password == member.Password;
            bool isAccountCorrect = login.Account.Contains(member.Account);
            bool isPasswordCorrect = login.Password == member.Password;
            return isAccountCorrect && isPasswordCorrect;
        }

        // GET: api/Gyms/5
        [HttpGet]
        public async Task<IEnumerable<GymListDTO>> GetGymFilter(string? target, string? targetItem, int? id)
        {
            if (id == null)
            {
                var gym = _context.Gym.Include(a => a.Area).Select(c => new GymListDTO
                {
                    GymId = c.GymId,
                    GymName = c.GymName,
                    Area = c.Area.Village,
                    Web = c.Web,
                    ImgPath = c.ImgPath,
                    Phone = c.Phone,
                });
                if (targetItem == "Area" && !string.IsNullOrEmpty(target))
                {
                    gym = gym.Where(g => g.Area.Contains(target));
                }
                else if (targetItem == "GymName" && !string.IsNullOrEmpty(target))
                {
                    gym = gym.Where(g => g.GymName.Contains(target));
                }
                return gym;
            }
            else
            {
                var gym = await _context.Gym.FindAsync(id);
                GymListDTO gymDTO = new GymListDTO
                {
                    GymId = gym.GymId,
                    GymName = gym.GymName,
                    Web = gym.Web,
                    ImgPath = gym.ImgPath,
                    Phone = gym.Phone,
                };
                return (IEnumerable<GymListDTO>)gymDTO;
            }

        }
        [HttpGet("{id}")]
        public async Task<Gym> GetGym(int id)
        {
            return await _context.Gym.FindAsync(id);

        }
        // PUT: api/Gyms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGym(int id, Gym gym)
        {
            if (id != gym.GymId)
            {
                return BadRequest();
            }

            _context.Entry(gym).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymExists(id))
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
        [HttpGet("Area")]
        public async Task<ActionResult<IEnumerable<Area>>>GetArea()
        {
            return await _context.Area.ToListAsync();
        }

        // POST: api/Gyms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Gym> PostGym(GymRegisterDTO gym)
        {
            Gym newGym = new Gym
            {
                GymId = 0,
                AreaId = gym.AreaId,
                GymName = gym.GymName,
                Address = gym.Address,
                Phone = gym.Phone,
                ImgPath = null,
                Web = null,
                Account = gym.Account,
                Password = gym.Password,
            };
            _context.Gym.Add(newGym);
            await _context.SaveChangesAsync();
            return newGym;
        }

        // DELETE: api/Gyms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gym = await _context.Gym.FindAsync(id);
            if (gym == null)
            {
                return NotFound();
            }

            _context.Gym.Remove(gym);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymExists(int id)
        {
            return _context.Gym.Any(e => e.GymId == id);
        }
    }
}
