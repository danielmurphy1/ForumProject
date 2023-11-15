using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.DatabaseServices.RepliesServices;
using Microsoft.AspNetCore.Authorization;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly ForumDataContext _context;
        private readonly PostRepliesService _postRepliesService;

        public RepliesController(ForumDataContext context, PostRepliesService postRepliesService)
        {
            _context = context;
            _postRepliesService = postRepliesService;
        }

        // GET: api/Replies
        //For Testing Only
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReplies()
        {
          if (_context.Replies == null)
          {
              return NotFound();
          }
            return await _context.Replies.ToListAsync();
        }

        // GET: api/Replies/5
        //For Testing Only
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reply>> GetReply(int id)
        {
          if (_context.Replies == null)
          {
              return NotFound();
          }
            var reply = await _context.Replies.FindAsync(id);

            if (reply == null)
            {
                return NotFound();
            }

            return reply;
        }

        // POST: api/Replies
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Reply>> PostReply(Reply reply)
        {
            try
            {
                var newReply = await _postRepliesService.AddReply(reply);
                return CreatedAtAction("PostReply", new { id = reply.Id }, newReply);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
