using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using iSpan_final_service.DTO;
using NuGet.Protocol.Plugins;

namespace iSpan_final_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly WOBContext _context;

        public MembersController(WOBContext context)
        {
            _context = context;
        }

        //------------------------------------------------------------------------------
        // 【獲取所有資料集合】
        //------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        //------------------------------------------------------------------------------
        // 【獲取個人文章】
        //------------------------------------------------------------------------------
        [HttpGet("getMyArticle")]
        public async Task<IEnumerable<myArticleDTO>> GetArticleAndMember(int id)
        {
            return _context.Article.Join(_context.Member, a => a.MemberId, mem => mem.MemberId,
               (a, mem) => new myArticleDTO
               {
                   MemberId = a.MemberId,
                   Title = a.Title,
                   Context = a.Context,
                   Time = a.Time,
               }).Where(ccc => ccc.MemberId == id).Select(art => new myArticleDTO
               {
                   MemberId = art.MemberId,
                   Title = art.Title,
                   Context = art.Context,
                   Time = art.Time,
               });
        }


        //------------------------------------------------------------------------------
        // 【查詢】
        //------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }
            return member;
        }


        //------------------------------------------------------------------------------
        // 【登入】
        //------------------------------------------------------------------------------
        [HttpPost("Login")]
        public async Task<LoginDTO> LoginMember([FromBody] LoginDTO member)
        {
            //var aa = _context.Member;
            //var aaa = _context.Member.Where(login => CheckLoginInfo(member, login));
            return _context.Member.Where(login => login.Account.Contains(member.Account) && login.Password == member.Password).Select(login => new LoginDTO
            {
                MemberId = login.MemberId,
                Account = login.Account,
                Password = login.Password,
                //Name = login.Name,
                //Email = login.Email,
            }).First();
        }
        private static bool CheckLoginInfo(LoginDTO member, Member login)
        {
            //return login.Email.Contains(member.Email) && login.Password == member.Password;
            bool isAccountCorrect = login.Account.Contains(member.Account);
            bool isPasswordCorrect = login.Password == member.Password;
            return isAccountCorrect && isPasswordCorrect;
        }


        //------------------------------------------------------------------------------
        // 【忘記密碼】
        //------------------------------------------------------------------------------
        [HttpGet("Forget")]
        public async Task<LoginDTO> ForgetPassword(String FA)
        {
            //var aa = _context.Member;
            //var aaa = _context.Member.Where(login => CheckLoginInfo(member, login));

            return _context.Member.Where(c => c.Account.Contains(FA)).Select(c => new LoginDTO
            {
                MemberId = c.MemberId,
                Account = c.Account,
                Password = c.Password,
                Name = c.Name,
                Email = c.Email,
            }).FirstOrDefault();
        }

        [HttpPatch("Send")]
        public async Task<string> PutPassword(int id, string NPassword)
        {
            //var aa = _context.Member;
            //var aaa = _context.Member.Where(login => CheckLoginInfo(member, login));

            Member member = await _context.Member.FindAsync(id);
            member.Password=NPassword;
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "請前往信箱確認";
        }

        //------------------------------------------------------------------------------
        // 【註冊】
        //------------------------------------------------------------------------------
        [HttpPost]
        public async Task<Member> Post(RegisterDTO Register)
        {
            Member new_mem = new Member
            {
                MemberId = Register.MemberId,
                Account = Register.Account,
                Password = Register.Password,
                Name = Register.Name,
                Mobile = Register.Mobile,
                Gender = Register.Gender,
                Picture = "0000.jpg",
                Email = Register.Email,
                Authority = Register.Authority,

            };
            _context.Member.Add(new_mem);
            await _context.SaveChangesAsync();
            return new_mem;
        }

        //------------------------------------------------------------------------------   
        // 【修改】
        //------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<string> PutMemberToCoach(int id, ModifyDTO empDTO)
        {
            if (id != empDTO.MemberId)
            {
                return "ID不正確";
            }
            Member emp = await _context.Member.FindAsync(empDTO.MemberId);

            emp.Name = empDTO.Name;
            emp.Password= empDTO.Password;
            emp.Mobile = empDTO.Mobile;
            emp.Price = empDTO.Price;
            emp.Address = empDTO.Address;
            emp.Email = empDTO.Email;
            emp.Skill = empDTO.Skill;
            emp.SelfIntro = empDTO.selfIntro;
            emp.Age = empDTO.Age;

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        [HttpPatch("{id}")]
        public async Task<string> ProfilePic(int id, ModifyDTO empDTO)
        {
            if (id != empDTO.MemberId)
            {
                return "ID不正確";
            }
            Member emp = await _context.Member.FindAsync(empDTO.MemberId);
            emp.MemberId = empDTO.MemberId;
            emp.Picture = empDTO.picture;
            emp.Mobile = empDTO.Mobile;
            emp.Price = empDTO.Price;
            emp.Address = empDTO.Address;
            emp.Email = empDTO.Email;
            emp.Skill = empDTO.Skill;
            emp.Name = empDTO.Name;
            emp.SelfIntro = empDTO.selfIntro;
            emp.Authority = empDTO.Authority;
            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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
        //------------------------------------------------------------------------------
        // 【登出？】
        //------------------------------------------------------------------------------
        [HttpPut("Logout")]
        public async Task<ActionResult<Member>> LogOut(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        //------------------------------------------------------------------------------
        // 【刪除】
        //------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //------------------------------------------------------------------------------


        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
