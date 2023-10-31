using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int Replies { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public int BoardId { get; set; }
        public Board? Board { get; set; }
        public IEnumerable<Reply>? ReplyMessages { get; set; }
    }
}