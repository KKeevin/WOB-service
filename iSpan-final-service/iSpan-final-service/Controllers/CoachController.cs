using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata;
using NuGet.Configuration;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly WOBContext _context;

        public CoachController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Coach
        [HttpGet]
        public async Task<IEnumerable<CoachDTO>> GetMember()
        {
            return _context.Member.Select(emp => new CoachDTO
            {
                MemberId = emp.MemberId,
                Account = emp.Account,
                Name = emp.Name,
                Gender = emp.Gender,
                Age = emp.Age,
                Address = emp.Address,
                Skill = emp.Skill,
                Price = emp.Price,
                Picture = emp.Picture,
                SelfIntro = emp.SelfIntro,
            });
        }

        // GET: api/Coach/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoachDTO>> GetMember(int id)
        {
            var member = await _context.Member.FindAsync(id);


            CoachDTO EmpDTO = new CoachDTO
            {
                Account = member.Account,
                Name = member.Name,
                Age = member.Age,
                Gender = member.Gender,
                Address = member.Address,
                Skill = member.Skill,
                Price = member.Price,
                Picture = member.Picture,
                SelfIntro = member.SelfIntro,
            };
            if (member == null)
            {
                return NotFound();
            }

            return EmpDTO;
        }

        // PUT: api/Coach/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutMember(string Account, CoachDTO EmpDTO)
        {
            if (Account != EmpDTO.Account)
            {
                return "ID不正確";
            }
            Member emp = await _context.Member.FindAsync(EmpDTO.Account);
            emp.Name = EmpDTO.Name;
            emp.Age = EmpDTO.Age;
            emp.Gender = EmpDTO.Gender;
            emp.Address = EmpDTO.Address;
            emp.Skill = EmpDTO.Skill;
            emp.Price = EmpDTO.Price;
            emp.Picture = EmpDTO.Picture;
            emp.SelfIntro = EmpDTO.SelfIntro;
            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Account))
                {
                    return "找不到記錄";
                }
                else
                {
                    throw;
                }
            }

            return "修改成功";
        }

        // POST: api/Coach
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Coach/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteMember(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return "找不到要刪除的記錄";
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return "刪除成功";
        }
        [HttpPost("Filter")]
        //Uri:api/Coach/Filter
        public async Task<IEnumerable<CoachDTO>> FilterCoach([FromBody] CoachDTO Coach)
        {
            return _context.Member.Where(emp => emp.Name.Contains(Coach.Name) && emp.Authority >= 200).Select(emp => new CoachDTO
            {
                MemberId = emp.MemberId,
                Account = emp.Account,
                Name = emp.Name,
                Age = emp.Age,
                Gender = emp.Gender,
                Address = emp.Address,
                Skill = emp.Skill,
                Price = emp.Price,
                Picture = emp.Picture,
                SelfIntro = emp.SelfIntro,
            }) ; 
        }

        [HttpGet("List")]
        //Uri:api/Coach/Filter
        public async Task<IEnumerable<CoachDTO>> ListCoach()
        {
            return _context.Member.Where(emp => emp.Authority >= 200).Select(emp => new CoachDTO
            {
                MemberId = emp.MemberId,
                Account = emp.Account,
                Name = emp.Name,
                Age= emp.Age,
                Gender = emp.Gender,
                Address = emp.Address,
                Skill = emp.Skill,
                Price = emp.Price,
                Picture = emp.Picture,
                SelfIntro = emp.SelfIntro,

            });
        }

        [HttpGet("Single")]
        //Uri:api/Coach/Single
        public async Task<CoachDTO> SingleCoach(int id)
        {
            var aaa = _context.Member.Where(emp => emp.MemberId == id).ToList().Select(emp => new CoachDTO
            {
                MemberId = emp.MemberId,
                Account = emp.Account,
                Name = emp.Name,
                Age = emp.Age,
                Gender = emp.Gender,
                Address = emp.Address,
                Skill = emp.Skill,
                Price = emp.Price,
                Picture = emp.Picture,
                SelfIntro = emp.SelfIntro,
                Email= emp.Email,
 

            }).SingleOrDefault();
            return aaa;
        }

        [HttpPatch("{id}")]
        //Uri:api/Coach/Patch
        public async Task<string> PatchCoach(int id, CoachDTO empDTO)
        {
            if (id != empDTO.MemberId)
            {
                return "ID不正確";
            }            
            
            Member emp = _context.Member.Where(emp => emp.MemberId == id).First();
            emp.MemberId = empDTO.MemberId;
            emp.Authority = 200;
            emp.SelfIntro= empDTO.SelfIntro;
            emp.Name= emp.Name;
            emp.Skill= empDTO.Skill;
            emp.Address= empDTO.Address;
            emp.Email = empDTO.Email;
            emp.Price = empDTO.Price;
            emp.SurviceArea = null;



            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists2(id))
                {
                    return "找不到欲修改資料";
                }
                else
                {
                    throw;
                }
            }
            return "修改成功!!";
        }
        private bool MemberExists(string name)
        {
            return _context.Member.Any(e => e.Name == name);
        }

        private bool MemberExists2(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
