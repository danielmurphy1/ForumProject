using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ForumProject.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
