using ForumProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumProject.Models;
using Microsoft.EntityFrameworkCore;
using ForumProject.DataTransferObjects;
using ForumProject.DatabaseServices.PostsServices;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ForumDataContext _context;
        private readonly GetPostsService _getPostsService;
        private readonly PostPostsService _postPostsService;

        public PostsController(ForumDataContext context, GetPostsService getPostsService, PostPostsService postPostsService)
        {
            _context = context;
            _getPostsService = getPostsService;
            _postPostsService = postPostsService;
        }

        //GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }


        //GET: api/Posts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetSinglePost(int id)
        {
            try
            {
                var post = await _getPostsService.GetSinglePostWithUserAndReplies(id);
                return Ok(post);
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }

        //POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            try
            {
                var newPost = await  _postPostsService.AddPost(post);
                return CreatedAtAction("PostsPost", new { id = post.Id }, newPost);
            } 
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("withusers")]
        //public async Task<ActionResult<IEnumerable<PostDTO>>> GetWithUsers()
        //{
        //    //var withUsers = await _context.Posts.Include(x => x.User)
        //    //    .Where(p => p.BoardId == 1).ToListAsync();
        //    //return withUsers;

        //    var postEntities = await _context.Posts.Include(x => x.User)
        //        .OrderBy(x => x.CreatedAt)
        //        .ToListAsync();

        //    var postDTOs = new List<PostDTO>();

        //    for (int i = 0; i < postEntities.Count; i++)
        //    {
        //        var postEntity = postEntities[i];
        //        postDTOs.Add(new PostDTO
        //        {
        //            Id = postEntity.Id,
        //            Title = postEntity.Title,
        //            Body = postEntity.Body,
        //            User = new UserDTO
        //            {
        //                Id = postEntity.User.Id,
        //                Username = postEntity.User.Username,
        //                CreatedAt = postEntity.User.CreatedAt
        //            }
        //        });
        //    }

        //    return postDTOs;
        //}


        //GET: api/Posts/boardposts/1
        //[HttpGet("boardposts/{boardId}")]
        //public async Task<ActionResult<IEnumerable<Post>>> GetBoardPosts(int boardId)
        //{
        //    var tempPosts = _context.Posts
        //        .Include(p => p.User)
        //        .Where(p => p.BoardId == boardId)
        //        .OrderBy(p => p.CreatedAt);
        //    return await tempPosts.ToListAsync();

        //    //var tempPosts = _context.Posts
        //    //    .Where(p => p.BoardId == id);
        //    //return await tempPosts.ToListAsync();

        //    //var title = await _context.Boards.Where(b => b.Title == id).FirstOrDefaultAsync();
        //    //var tempPosts = _context.Posts
        //    //    .Where(p => p.BoardId == title.Id);
        //    //return await tempPosts.ToListAsync();

        //    //var posts =  _context.Posts
        //    //    .Join(_context.Boards.Where(b => b.Title == board),
        //    //    p => p.BoardId,
        //    //    b => b.Id,
        //    //    (p, b) => new { Posts = p, Boards = b });
        //    //return Ok(await posts.ToListAsync());
        //}
    }
}
