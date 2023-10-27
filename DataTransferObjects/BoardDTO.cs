using ForumProject.Models;

namespace ForumProject.DataTransferObjects
{
    public class BoardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl {  get; set; }
        public int? Topics { get; set; }
        public IEnumerable<PostDTO>? Posts { get; set; }
        public PostDTO? LastPost { get; set; }
    }
}
