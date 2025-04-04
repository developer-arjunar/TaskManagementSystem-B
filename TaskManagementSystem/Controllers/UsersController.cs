using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.DTOs.Responses;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRequest saveRequest)
        {
            var user = new User()
            {
                Name = saveRequest.Name,
                Email = saveRequest.Email,
                Username = saveRequest.Username.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(saveRequest.Password),
                RoleId = saveRequest.RoleId
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var savedUser = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == user.Id);

            if (savedUser == null)
            {
                return BadRequest();
            }

            var saveResponse = new UserResponse
            {
                Id = savedUser.Id,
                Name = savedUser.Name,
                Email = savedUser.Email,
                Username = savedUser.Username.ToUpper(),
                Role = savedUser.Role
            };

            return Ok(saveResponse);
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public IActionResult AuthenticateUser(UserAuthenticateRequest userAuthenticateRequest)
        {
            string storedHash = "";

            var filterUser = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Username == userAuthenticateRequest.Username);

            if (filterUser is not null)
            {
                storedHash = filterUser.Password;

                if (BCrypt.Net.BCrypt.Verify(userAuthenticateRequest.Password, storedHash))
                {
                    var authResponse = new UserResponse
                    {
                        Id = filterUser.Id,
                        Name = filterUser.Name,
                        Email = filterUser.Email,
                        Username = filterUser.Username.ToUpper(),
                        Role = filterUser.Role
                    };

                    return Ok(authResponse);
                } else
                {
                    return BadRequest();
                }
            } else
            {
                return NotFound();
            }
        }
    }
}
