using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using Microsoft.AspNetCore.Cors;
using iSpan_final_service.DTO;

namespace iSpan_final_service.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly WOBContext _context;

        public RepliesController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Replies
        [HttpGet]
        public async Task<IEnumerable<ReplyDTO>> GetReply()
        {
            return _context.Reply.Select(reply => new ReplyDTO
            {
                MemberId = reply.MemberId,
                ArticleId = reply.ArticleId,
                Context = reply.Context,
                Time = reply.Time,
                NumNice = reply.NumNice,
            });
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTO>> GetReply(int id)
        {
            var reply = await _context.Reply.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            ReplyDTO RplDTO = new ReplyDTO
            {
                MemberId = reply.MemberId,
                ArticleId = reply.ArticleId,
                Context = reply.Context,
                Time = reply.Time,
                NumNice = reply.NumNice,
            };
            return RplDTO;
        }


        [HttpPost("Filter")]    //Uri: api/Employees/Filter
        public async Task<IEnumerable<ReplyDTO>> FilterReply([FromBody] ReplyDTO reply)
        {
            return await _context.Reply.Where(
                rpl => rpl.ArticleId == reply.ArticleId).Join(_context.Member, rpl => rpl.MemberId, mem => mem.MemberId,
            (rpl, mem) => new ReplyDTO
            {
                ReplyId = rpl.ReplyId,
                ArticleId = rpl.ArticleId,
                MemberId = rpl.MemberId,
                Account = mem.Account,
                Context = rpl.Context,
                Time = rpl.Time,
                NumNice = rpl.NumNice,
                Picture = mem.Picture,

            }).ToListAsync();


            /*
            return _context.Reply.Where(
                rpl => rpl.ArticleId == reply.ArticleId).Select(rpl => new ReplyDTO
                {
                    ReplyId = rpl.ReplyId,
                    ArticleId = rpl.ArticleId,
                    MemberId = rpl.MemberId,
                    Context = rpl.Context,
                    Time = rpl.Time,
                    NumNice = rpl.NumNice,
                });
            */
        }

        // PUT: api/Replies/5
        [HttpPut("{id}")]
        public async Task<string> PutReply(int id, ReplyDTO RplDTO)
        {
            if (id != RplDTO.ReplyId)
            {
                return "ID不正確!";
            }

            Reply rpl = await _context.Reply.FindAsync(RplDTO.ReplyId);

            rpl.ArticleId = (int)RplDTO.ArticleId;
            rpl.MemberId = (int)RplDTO.MemberId;
            rpl.Context = RplDTO.Context;
            //rpl.Time = (DateTime)RplDTO.Time;
            rpl.NumNice = (int)RplDTO.NumNice;

            _context.Entry(rpl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
                {
                    return "找不到欲修改的留言!";
                }
                else
                {
                    throw;
                }
            }

            return "修改成功!";
        }

        // POST: api/Replies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostReply(ReplyDTO reply)
        {
            Reply rpl = new Reply
            {
                ArticleId = (int)reply.ArticleId,
                MemberId = (int)reply.MemberId,

                Context = reply.Context,
                NumNice = (int)reply.NumNice,
            };

            _context.Reply.Add(rpl);
            await _context.SaveChangesAsync();

            return "留言成功";
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteReply(int id)
        {
            var reply = await _context.Reply.FindAsync(id);
            if (reply == null)
            {
                return "找不到欲刪除的留言";
            }

            _context.Reply.Remove(reply);
            await _context.SaveChangesAsync();

            return "刪除成功!";
        }





        private bool ReplyExists(int id)
        {
            return _context.Reply.Any(e => e.ReplyId == id);
        }
    }
}
