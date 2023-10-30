using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.DataTransferObjects;
using ForumProject.DatabaseServices.BoardsServices;
using NuGet.Protocol;
using System.Data.Common;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly ForumDataContext _context;
        private readonly GetBoardsService _getBoardsService;

        public BoardsController(ForumDataContext context, GetBoardsService getBoardsService)
        {
            _context = context;
            _getBoardsService = getBoardsService;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<BoardDTO>> GetBoards()
        {
            var boards = await _getBoardsService.GetBoardsWithLastPostInfo();
            return Ok(boards);
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDTO>> GetBoard(int id)
        {
            try
            {
                var board = await _getBoardsService.GetSingleBoard(id);
                return Ok(board);

            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }

        }
        

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
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

        // POST: api/Boards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {
          if (_context.Boards == null)
          {
              return Problem("Entity set 'ForumDataContext.Boards'  is null.");
          }
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoard", new { id = board.Id }, board);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(int id)
        {
            return (_context.Boards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
