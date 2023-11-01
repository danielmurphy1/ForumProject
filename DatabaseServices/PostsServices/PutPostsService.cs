using ForumProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.DatabaseServices.PostsServices
{
    public class PutPostsService
    {
        private readonly ForumDataContext _context;
        public PutPostsService(ForumDataContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> UpdatePostViewsOrReplies(int id, string property)
        {
            var selectedPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (property == "Replies" && selectedPost != null)
            {
                //updateProperty = selectedPost.Replies;
                selectedPost.Replies += 1;

            }
            else if (property == "Views" && selectedPost != null)
            {
                //updateProperty = selectedPost.Views;
                selectedPost.Views += 1;

            }
            else
            {
                //return BadRequest();
                throw new BadHttpRequestException("Bad Request");
            }
            _context.Entry(selectedPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
