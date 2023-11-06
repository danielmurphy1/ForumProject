using ForumProject.Data;
using ForumProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.DatabaseServices.UsersServices
{
    public class PostUsersServices
    {
        private readonly ForumDataContext _context;

        public PostUsersServices(ForumDataContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            var users = await _context.Users.ToListAsync();
            foreach(var u  in users)
            {
                if((u.Username == user.Username || u.Email == user.Email))
                {
                    throw new DbUpdateException("Username or Email Already Registered");
                }
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
