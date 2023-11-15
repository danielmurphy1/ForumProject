using ForumProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumProject.Models;
using Microsoft.EntityFrameworkCore;
using ForumProject.DataTransferObjects;
using ForumProject.DatabaseServices.PostsServices;
using Microsoft.AspNetCore.Authorization;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ForumDataContext _context;
        private readonly GetPostsService _getPostsService;
        private readonly PostPostsService _postPostsService;
        private readonly PutPostsService _putPostsService;

        public PostsController(ForumDataContext context, 
                                GetPostsService getPostsService, 
                                PostPostsService postPostsService,
                                PutPostsService putPostService)
        {
            _context = context;
            _getPostsService = getPostsService;
            _postPostsService = postPostsService;
            _putPostsService = putPostService;
        }

        //GET: api/Posts
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }


        //GET: api/Posts/1
        [Authorize]
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
        [Authorize]
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

        // PUT: api/Posts/5/update_reply
        [Authorize]
        [HttpPut("{id}/{property}")]
        //public async Task<IActionResult> PutPostReplies(int id, Post post)
        public async Task<IActionResult> PutPost(int id, string property)

        {
            try
            {
                await _putPostsService.UpdatePostViewsOrReplies(id, property);
                return NoContent();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest();
            }

            
        }

        
    }
}
