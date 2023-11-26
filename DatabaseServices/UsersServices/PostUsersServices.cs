using ForumProject.Data;
using ForumProject.DataTransferObjects;
using ForumProject.Exceptions;
using ForumProject.Models;
using ForumProject.PasswordHasher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumProject.DatabaseServices.UsersServices
{
    public class PostUsersServices
    {
        private readonly ForumDataContext _context;
        private readonly IConfiguration _configuration;

        public PostUsersServices(ForumDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            user.Password = Hasher.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserDTO> AuthenticateUser(UserDTO user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (checkUser == null)
            {
                throw new NotFoundException("User Not Found");
            }
            else if (!Hasher.VerifyPassword(user.Password, checkUser.Password!))
            {
                throw new BadRequestException("Incorrect Password");
            }
            else
            {
                var userDTO = new UserDTO
                {
                    Id = checkUser.Id,
                    Username = checkUser.Username,
                    Token = CreateJwt(checkUser)
                };
                return userDTO;
            }
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("MySecretKeyForDevelopment");
            var secret = _configuration.GetSection("JWTSecretKey").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
