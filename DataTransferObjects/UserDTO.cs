using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
