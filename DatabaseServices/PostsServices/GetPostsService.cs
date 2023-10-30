using ForumProject.Data;
using ForumProject.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.DatabaseServices.PostsServices
{
    public class GetPostsService
    {
        private readonly ForumDataContext _context;

        public GetPostsService(ForumDataContext context)
        {
            _context = context;
        }

        public async Task<PostDTO> GetSinglePostWithUserAndReplies(int id)
        {
            var post = await _context.Posts
               .Include(p => p.User)
               .Include(p => p.ReplyMessages)
               .Include("ReplyMessages.User")
               .FirstOrDefaultAsync(p => p.Id == id);
            var replies = post.ReplyMessages.ToList();
            var replyDTOs = new List<ReplyDTO>();
            foreach (var reply in replies)
            {
                replyDTOs.Add(new ReplyDTO
                {
                    Id = reply.Id,
                    Body = reply.Body,
                    CreatedAt = reply.CreatedAt,
                    PostId = reply.PostId,
                    User = new UserDTO()
                    {
                        Id = reply.User.Id,
                        Username = reply.User.Username
                    }
                });
            }
            replyDTOs.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));

            var postDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Views = post.Views,
                Replies = post.Replies,
                BoardId = post.BoardId,
                User = new UserDTO
                {
                    Id = post.User.Id,
                    Username = post.User.Username
                },
                ReplyMessages = replyDTOs
            };
            return postDTO;
        }
    }
}
