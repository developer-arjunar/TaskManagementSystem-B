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

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = dbContext.Users.ToList();

            var getAllResponse = users.Select(user => new UserSimpleResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Status = user.Status
            }).ToList();

            return Ok(getAllResponse);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRequest saveRequest)
        {
            var user = new User()
            {
                Name = saveRequest.Name,
                Email = saveRequest.Email,
                PhoneNo = saveRequest.PhoneNo,
                Username = saveRequest.Username.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(saveRequest.Password),
                Status = Enums.UserStatus.ACTIVE,
                RoleId = saveRequest.RoleId
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var savedUser = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == user.Id);

            if (savedUser is null)
            {
                return BadRequest();
            }

            var saveResponse = new UserResponse
            {
                Id = savedUser.Id,
                Name = savedUser.Name,
                Email = savedUser.Email,
                PhoneNo = savedUser.PhoneNo,
                Username = savedUser.Username.ToUpper(),
                Status = savedUser.Status,
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
                        PhoneNo = filterUser.PhoneNo,
                        Username = filterUser.Username.ToUpper(),
                        Status = filterUser.Status,
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
