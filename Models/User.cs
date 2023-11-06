using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool Admin { get; set; } = false;
        public IEnumerable<Post>? Posts { get; set; }
        public IEnumerable<Reply>? Replies { get; set; }
    }
}