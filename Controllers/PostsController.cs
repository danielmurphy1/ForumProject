using ForumProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ForumDataContext _context;

        public PostsController(ForumDataContext context)
        {
            _context = context;
        }

        //GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }


        //GET: api/Posts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetSinglePost(int id)
        {
            if(_context.Posts == null)
            {
                return NotFound();
            }
            var post =  await _context.Posts.FindAsync(id);
            if(post == null)
            {
                return NotFound();
            }
            return post;
        }


        //GET: api/Posts/boards/1
        [HttpGet("boards/{boardId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetBoardPosts(int boardId)
        {
            var tempPosts = _context.Posts
                .Where(p => p.BoardId == boardId);
            return await tempPosts.ToListAsync();

            //var tempPosts = _context.Posts
            //    .Where(p => p.BoardId == id);
            //return await tempPosts.ToListAsync();

            //var title = await _context.Boards.Where(b => b.Title == id).FirstOrDefaultAsync();
            //var tempPosts = _context.Posts
            //    .Where(p => p.BoardId == title.Id);
            //return await tempPosts.ToListAsync();

            //var posts =  _context.Posts
            //    .Join(_context.Boards.Where(b => b.Title == board),
            //    p => p.BoardId,
            //    b => b.Id,
            //    (p, b) => new { Posts = p, Boards = b });
            //return Ok(await posts.ToListAsync());
        }
    }
}
