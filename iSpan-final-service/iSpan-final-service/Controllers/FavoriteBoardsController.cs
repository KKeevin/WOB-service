using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpan_final_service.Models;
using Microsoft.AspNetCore.Cors;

namespace iSpan_final_service.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteBoardsController : ControllerBase
    {
        private readonly WOBContext _context;

        public FavoriteBoardsController(WOBContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteBoards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteBoard>>> GetFavoriteBoard()
        {
            return await _context.FavoriteBoard.ToListAsync();
        }

        /*
        // GET: api/FavoriteBoards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteBoard>> GetFavoriteBoard(int id)
        {
            var favoriteBoard = await _context.FavoriteBoard.FindAsync(id);

            if (favoriteBoard == null)
            {
                return NotFound();
            }

            return favoriteBoard;
        }

       
        // PUT: api/FavoriteBoards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteBoard(int id, FavoriteBoard favoriteBoard)
        {
            if (id != favoriteBoard.BoardId)
            {
                return BadRequest();
            }

            _context.Entry(favoriteBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteBoardExists(id))
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

        // POST: api/FavoriteBoards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteBoard>> PostFavoriteBoard(FavoriteBoard favoriteBoard)
        {
            _context.FavoriteBoard.Add(favoriteBoard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteBoard", new { id = favoriteBoard.BoardId }, favoriteBoard);
        }

        // DELETE: api/FavoriteBoards/5
        [HttpDelete("{boardId}/{memberId}")]
        public async Task<IActionResult> DeleteFavoriteBoard(int boardId, int memberId)
        {
            var favoriteBoard = await _context.FavoriteBoard.FindAsync(boardId, memberId);
            if (favoriteBoard == null)
            {
                return NotFound();
            }

            _context.FavoriteBoard.Remove(favoriteBoard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteBoardExists(int id)
        {
            return _context.FavoriteBoard.Any(e => e.BoardId == id);
        }
    }
}
