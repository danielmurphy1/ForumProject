using ForumProject.Data;
using ForumProject.Models;

namespace ForumProject.DatabaseServices.RepliesServices
{
    public class PostRepliesService
    {
        private readonly ForumDataContext _context;

        public PostRepliesService(ForumDataContext context)
        {
            _context = context;
        }

        public async Task<Reply> AddReply(Reply reply)
        {
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply;
        }
    }
}
