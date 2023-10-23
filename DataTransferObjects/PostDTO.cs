using ForumProject.Models;

namespace ForumProject.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UserDTO User { get; set; }
    }
}
