using ForumProject.Data;
using ForumProject.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.DatabaseServices.BoardsServices
{
    public class GetBoardsService
    {
        private readonly ForumDataContext _context;

        public GetBoardsService(ForumDataContext context)
        {
            _context = context;
        }

        public async Task<List<BoardDTO>> GetBoardsWithLastPostInfo()
        {
            var boards = await _context.Boards
                .Include(b => b.Posts).Include("Posts.User").ToListAsync();
            
            var boardDTOs = new List<BoardDTO>();
            foreach (var board in boards)
            {
                var boardPosts = board.Posts.ToList();
                //need different DTO form whether there are Posts or not - might be another way to differentiate
                if(boardPosts.Count > 0)
                {
                    boardPosts.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));


                    var postDTO = new PostDTO
                    {
                        Id = boardPosts.LastOrDefault().Id,
                        Title = boardPosts.LastOrDefault().Title,
                        Body = boardPosts.LastOrDefault().Body,
                        CreatedAt = boardPosts.LastOrDefault().CreatedAt,
                        User = new UserDTO
                        {
                            Id = boardPosts.LastOrDefault().User.Id,
                            Username = boardPosts.LastOrDefault().User.Username
                        }
                    };
                    boardDTOs.Add(new BoardDTO
                    {
                        Id = board.Id,
                        Title = board.Title,
                        Description = board.Description,
                        ImgUrl = board.ImgUrl,
                        Topics = board.Posts.Count(),
                        LastPost = postDTO
                    });
                }
                else
                {
                    boardDTOs.Add(new BoardDTO
                    {
                        Id = board.Id,
                        Title = board.Title,
                        Description = board.Description,
                        ImgUrl = board.ImgUrl,
                        Topics = board.Posts.Count()
                    });
                }
               
            }
            return boardDTOs;
        }

        public async Task<BoardDTO> GetSingleBoard(int id)
        {
            var board = await _context.Boards
                .Include(b => b.Posts)
                .Include("Posts.User")
                .FirstOrDefaultAsync(b => b.Id == id);
            var boardPosts = board.Posts.ToList();
            var postDTOs = new List<PostDTO>();
            foreach(var post in boardPosts)
            {
                postDTOs.Add(new PostDTO
                {
                    Id = post.Id,
                    Title = post.Title,
                    Replies = post.Replies,
                    Views = post.Views,
                    CreatedAt = post.CreatedAt,
                    User = new UserDTO
                    {
                        Id = post.User.Id,
                        Username = post.User.Username
                    }
                });
            }
            postDTOs.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));
            var boardDTO = new BoardDTO
            {
                Id = board.Id,
                Title = board.Title,
                Description = board.Description,
                ImgUrl = board.ImgUrl, 
                Posts = postDTOs
            };

            return boardDTO;
        }
    }
}
