using ForumProject.Data;
using ForumProject.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<BoardDTO>> GetBoardsWithPostInfo()
        {
            var boards = await _context.Boards
                .Include(b => b.Posts).Include("Posts.User").ToListAsync();
            var boardDTOs = new List<BoardDTO>();
            foreach (var board in boards)
            {
                var postDTOs = new List<PostDTO>();
                for (int i = 0; i < board.Posts.Count(); i++)
                {
                    //var boardPost = board.Posts.ElementAt(i);
                    postDTOs.Add(new PostDTO
                    {
                        Id = board.Posts.ElementAt(i).Id,
                        Title = board.Posts.ElementAt(i).Title,
                        Body = board.Posts.ElementAt(i).Body, 
                        CreatedAt = board.Posts.ElementAt(i).CreatedAt,
                        User = new UserDTO
                        {
                            Id = board.Posts.ElementAt(i).User.Id,
                            Username = board.Posts.ElementAt(i).User.Username
                        }
                    });
                }
                //var ordered = postDTOs.OrderBy(p => p.CreatedAt).ToList();
                postDTOs.Sort((x,y) =>  x.CreatedAt.CompareTo(y.CreatedAt));
                boardDTOs.Add(new BoardDTO
                {
                    Id = board.Id,
                    Title = board.Title,
                    Description = board.Description,
                    ImgUrl = board.ImgUrl,
                    Topics = postDTOs.Count(),
                    Posts = postDTOs,
                    LastPost = postDTOs.LastOrDefault()
                });
            }
            return boardDTOs;
        }

        public async Task<BoardDTO> GetSingleBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            var boardDTO = new BoardDTO
            {
                Id = board.Id,
                Title = board.Title,
                Description = board.Description,
                ImgUrl = board.ImgUrl
            };

            return boardDTO;
        }
    }
}
