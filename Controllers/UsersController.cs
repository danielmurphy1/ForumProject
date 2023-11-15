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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ForumProject.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authorization;

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
        //For Testing Only
        [Authorize]
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
        //For Testing Only
        [Authorize]
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
                
        // POST: api/Users/signup
        [HttpPost("signup")]
        public async Task<ActionResult<User>> PostNewUser(User user)
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
        public async Task<ActionResult<UserDTO>> PostAuthUser(UserDTO user)
        {
            try
            {
                var checkedUser = await _postUsersServices.AuthenticateUser(user);
                return Ok(checkedUser);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
                
            }
        }
    }
}
