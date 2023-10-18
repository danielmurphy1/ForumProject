using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public Post Post { get; set; }
    }
}