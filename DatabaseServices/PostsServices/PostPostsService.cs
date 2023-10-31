using ForumProject.Data;
using ForumProject.Models;

namespace ForumProject.DatabaseServices.PostsServices
{
    public class PostPostsService
    {
        private readonly ForumDataContext _context;
        public PostPostsService(ForumDataContext context)
        {
            _context = context;
        }

        public async Task<Post> AddPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("PostPost", new { id = post.Id }, post);
            return post;
        }
    }
}
