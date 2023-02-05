using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using System.Collections;
using static Humanizer.In;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InbodiesController : ControllerBase
    {
        private readonly WOBContext _context;

        public InbodiesController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Inbodies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inbody>>> GetInbody()
        {
            return await _context.Inbody.ToListAsync();
        }

        // GET: api/Inbodies/5
        [HttpGet("{id}")]
        public async Task<Inbody> GetInbody(int id)
        {
            return _context.Inbody.Where(emp => emp.MemberId == id).Select(emp => new Inbody
            {
                InbodyId = emp.InbodyId,
                MemberId= emp.MemberId,
                Date=emp.Date,
                Height=emp.Height,
                Weight=emp.Weight,
                Smm=emp.Smm,
                Bfm=emp.Bfm,
                Bmi = Math.Round((emp.Weight / (emp.Height * emp.Height) * 10000),2),

            }).First();

            //return _context.Inbody.Where(emp => emp.MemberId == id).First();
            //超好用欸這個

        }

        [HttpPatch("{id}")]
        public async Task<string> ProfilePic(int id, inbodyDTO ibdDTO)
        {
            if (id != ibdDTO.MemberId)
            {
                return "ID不正確";
            }
            Inbody emp = _context.Inbody.Where(emp => emp.MemberId == id).First();
            emp.Date = DateTime.Now;
            emp.Height = ibdDTO.Height;
            emp.Weight = ibdDTO.Weight;
            emp.Smm = ibdDTO.Smm;
            emp.Bfm = ibdDTO.Bfm;
            emp.Bmi = Math.Round((emp.Weight / (emp.Height * emp.Height) * 10000), 2);
            emp.Whr = 0;
            emp.Bmr = 0;

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InbodyExists(id))
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


        [HttpPost]
        public async Task<Inbody> Post(addInbodyDTO addIbdDTO)
        {
            Inbody new_ibd = new Inbody
            {
                MemberId = addIbdDTO.MemberId,
                Date = DateTime.Now,
                Height = addIbdDTO.Height,
                Weight = addIbdDTO.Weight,
                Smm = addIbdDTO.Smm,
                Bfm = addIbdDTO.Bfm,
                Bmi = Math.Round((addIbdDTO.Weight / (addIbdDTO.Height * addIbdDTO.Height) * 10000), 2),
            };
            _context.Inbody.Add(new_ibd);
            await _context.SaveChangesAsync();
            return new_ibd;
        }


        // DELETE: api/Inbodies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInbody(int id)
        {
            var inbody = await _context.Inbody.FindAsync(id);
            if (inbody == null)
            {
                return NotFound();
            }

            _context.Inbody.Remove(inbody);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InbodyExists(int id)
        {
            return _context.Inbody.Any(e => e.InbodyId == id);
        }
    }
}
