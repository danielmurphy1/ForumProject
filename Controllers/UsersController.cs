using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.DatabaseServices.UsersServices;
using NuGet.Protocol;
using ForumProject.DataTransferObjects;

namespace ForumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ForumDataContext _context;
        private readonly PostUsersServices _postUsersServices;

        public UsersController(ForumDataContext context, PostUsersServices postUsersServices)
        {
            _context = context;
            _postUsersServices = postUsersServices;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/signup
        [HttpPost("signup")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var newUser = await _postUsersServices.AddUser(user);
                return CreatedAtAction("GetUser", new { id = user.Id }, newUser);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex);
            }
            catch (DbUpdateException ex)
            {
                //return 409 due to not being able to process due to the STATE of the TARGET
                return Conflict(new {ex.Message, StatusCodes.Status409Conflict});
            }

        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> AuthUser(UserDTO user)
        {
            //try
            //{
            //    var newUser = await _postUsersServices.AddUser(user);
            //    return CreatedAtAction("GetUser", new { id = user.Id }, newUser);
            //}
            //catch (BadHttpRequestException ex)
            //{
            //    return BadRequest(ex);
            //}
            //catch (DbUpdateException ex)
            //{
            //    //return 409 due to not being able to process due to the STATE of the TARGET
            //    return Conflict(new { ex.Message, StatusCodes.Status409Conflict });
            //}
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (checkUser == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            else if (checkUser.Password != user.Password)
            {
                return BadRequest(new { Message = "Incorrect Password" });
            }
            else
            {
                var userDTO = new UserDTO
                {
                    Id = checkUser.Id,
                    Username = checkUser.Username,
                };
                return Ok(userDTO);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
