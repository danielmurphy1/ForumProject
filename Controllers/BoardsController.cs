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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<BoardDTO>> GetBoards()
        {
            var boards = await _getBoardsService.GetBoardsWithLastPostInfo();
            return Ok(boards);
        }

        // GET: api/Boards/5
        [Authorize]
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
    }
}
