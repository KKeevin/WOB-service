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
//Emma
using System.Security.Principal;
//
using System.Runtime.CompilerServices;
using static Humanizer.In;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace iSpan_final_service.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly WOBContext _context;

        public ArticlesController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetArticle()
        {
            //return _context.Article.Select(atc => new ArticleDTO //原本的
            //from atc in _context.Article
            //join b in _context.Board on atc.BoardId equals b.BoardId

            return await _context.Article.Join(_context.Board, art => art.BoardId, bord => bord.BoardId,
            (art, bord) => new ArticleDTO
            {
                ArticleId = art.ArticleId,
                MemberId = art.MemberId,
                BoardId = art.BoardId,
                BoardName = bord.BoardName,
                Title = art.Title,
                Context = art.Context,
                Time = art.Time,
                //Time = atc.Time.ToLocalTime(),
                NumNice = art.NumNice,
                NumReply = art.NumReply,
            }).Join(_context.Member, artDTO => artDTO.MemberId, mem => mem.MemberId,
            (artDTO, mem) => new ArticleDTO
            {
                ArticleId = artDTO.ArticleId,
                MemberId = artDTO.MemberId,
                Account = mem.Account,
                BoardId = artDTO.BoardId,
                BoardName = artDTO.BoardName,
                Title = artDTO.Title,
                Context = artDTO.Context,
                Time = artDTO.Time,
                //Time = atc.Time.ToLocalTime(),
                NumNice = artDTO.NumNice,
                NumReply = artDTO.NumReply,
                Picture = mem.Picture,
            }).ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<ArticleDTO>> GetArticle(int id)
        {
            //var article = await _context.Article.FindAsync(id);
            return await _context.Article.Where(a => a.ArticleId == id).Include(c=>c.Board).Join(_context.Member, a => a.MemberId, b => b.MemberId,
                (a, b) => new ArticleDTO 
                {
                    ArticleId = a.ArticleId,
                    MemberId = a.MemberId,
                    Account = b.Account,
                    BoardId = a.BoardId,
                    Title = a.Title,
                    Context = a.Context,
                    Time = a.Time,
                    //Time = article.Time.ToLocalTime(),
                    NumNice = a.NumNice,
                    NumReply = a.NumReply,
                    BoardName = a.Board.BoardName,
                    Picture = b.Picture,
                }).ToListAsync();

            //if (article == null)
            //{
            //    return NotFound();
            //}
            
            //ArticleDTO AtcDTO = new ArticleDTO
            //{
            //    ArticleId = article.ArticleId,
            //    MemberId = article.MemberId,
            //    //BoardName = article.BoardName,
            //    BoardId = article.BoardId,
            //    Title = article.Title,
            //    Context = article.Context,
            //    Time = article.Time,
            //    //Time = article.Time.ToLocalTime(),
            //    NumNice = article.NumNice,
            //    NumReply = article.NumReply,
            //};

            //return AtcDTO;
        }




        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<string> PutArticle(int id, ArticleDTO atcDTO)
        {
            if (id != atcDTO.ArticleId)
            {
                return "ID不正確!";
            }
            Article atc = await _context.Article.FindAsync(atcDTO.ArticleId);
            atc.MemberId = atcDTO.MemberId;
            atc.BoardId = atcDTO.BoardId;
            atc.Title = atcDTO.Title;
            atc.Context = atcDTO.Context;
            atc.Time = atcDTO.Time;
            //atc.Time = atcDTO.Time.ToLocalTime();
            atc.NumNice = atcDTO.NumNice;
            atc.NumReply = atcDTO.NumReply;
            _context.Entry(atc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return "找不到欲修改的文章!";
                }
                else
                {
                    throw;
                }
            }

            return "修改成功!";
        }


        // POST: api/Articles
        [HttpPost]
        public async Task<string> PostArticle(ArticleDTO article)
        {
            Article atc = new Article
            {
                MemberId = article.MemberId,
                //BoardName = article.BoardName,
                BoardId = article.BoardId,
                Title = article.Title,
                Context = article.Context,
                Time = article.Time,
                //Time = article.Time.ToLocalTime(),
                NumNice = article.NumNice,
                NumReply = article.NumReply,
            };

            _context.Article.Add(atc);
            await _context.SaveChangesAsync();

            return "發文成功";
        }


        // POST: api/Employees/Filter
        [HttpPost("Filter")]
        public async Task<IEnumerable<ArticleDTO>> FilterArticle([FromBody] ArticleDTO article)
        {
            return await _context.Article.Where(
                art => art.BoardId == article.BoardId).Join(_context.Board, art => art.BoardId, bord => bord.BoardId,
             (art, bord) => new ArticleDTO
             {
                 ArticleId = art.ArticleId,
                 MemberId = art.MemberId,
                 BoardId = art.BoardId,
                 BoardName = bord.BoardName,
                 Title = art.Title,
                 Context = art.Context,
                 Time = art.Time,
                 //Time = atc.Time.ToLocalTime(),
                 NumNice = art.NumNice,
                 NumReply = art.NumReply,
             }).Join(_context.Member, artDTO => artDTO.MemberId, mem => mem.MemberId,
            (artDTO, mem) => new ArticleDTO
            {
                ArticleId = artDTO.ArticleId,
                MemberId = artDTO.MemberId,
                Account = mem.Account,
                BoardId = artDTO.BoardId,
                BoardName = artDTO.BoardName,
                Title = artDTO.Title,
                Context = artDTO.Context,
                Time = artDTO.Time,
                //Time = atc.Time.ToLocalTime(),
                NumNice = artDTO.NumNice,
                NumReply = artDTO.NumReply,
                Picture = mem.Picture,
            }).ToListAsync();

            /*
            return _context.Article.Where(
                atc => atc.BoardId == article.BoardId).Select(atc => new ArticleDTO
                {
                    ArticleId = atc.ArticleId,
                    MemberId = atc.MemberId,
                    //BoardName = article.BoardName,
                    BoardId = atc.BoardId,
                    Title = atc.Title,
                    Context = atc.Context,
                    Time = atc.Time,
                    //Time = atc.Time.ToLocalTime(),
                    NumNice = atc.NumNice,
                    NumReply = atc.NumReply,
                });
            */
        }



        //[HttpPost("Filter")]
        //public async Task<IEnumerable<MatchDTO>> FilterMatch([FromBody] MatchDTO match)
        //{
        //    return _context.Match.Include(c => c.Calendar).Where(c => c.Calendar.OrdererId == match.OrdererId).Select(c => new MatchDTO
        //    {
        //        MatchId = c.MatchId,
        //        //ObjectId = c.Calendar.ObjectId,
        //        OrdererId = c.Calendar.OrdererId,
        //        ObjectName = _context.Member.Where(a => a.MemberId == c.Calendar.ObjectId).Select(name => name.Name).FirstOrDefault(),
        //        ObjectPic = _context.Member.Where(a => a.MemberId == c.Calendar.ObjectId).Select(pic => pic.Picture).FirstOrDefault(),
        //        Payment = c.Payment,
        //        IsPaid = c.IsPaid,
        //        IsConfirmed = c.Calendar.IsConfirmed,
        //        Content = c.Calendar.MyContent,
        //        OrderTime = c.OrderTime,
        //        DealTime = c.DealTime,
        //        StartDateTime = c.Calendar.StartDateTime,
        //    });
        //}



        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return "找不到欲刪除的文章";
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return "刪除成功!";
        }


        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}
