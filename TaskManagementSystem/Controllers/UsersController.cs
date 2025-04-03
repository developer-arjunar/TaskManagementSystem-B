using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

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

            return Ok(user);
        }

        //[HttpPost]
        //[Route("{username}/{password}")]
        //public bool AuthenticateUser(string username, string password) 
        //{
        //    string storedHash = "";

        //    var filterUser = dbContext.Users.FirstOrDefault(u => u.Username == username);

        //    //string storedHash = BCrypt.Net.Get
        //    //bool isPasswordValid = false;

        //    if (filterUser != null)
        //    {
        //        storedHash = filterUser.Password;
        //    }

        //    return BCrypt.Net.BCrypt.Verify(password, storedHash);
        //}

        [HttpPost]
        [Route("AuthenticateUser")]
        public IActionResult AuthenticateUser(UserAuthenticateRequest userAuthenticateRequest)
        {
            string storedHash = "";

            var filterUser = dbContext.Users.FirstOrDefault(u => u.Username == userAuthenticateRequest.Username);

            if (filterUser is not null)
            {
                storedHash = filterUser.Password;

                //Console.WriteLine("Password Hash : ");
                //Console.ReadLine();
                
                //if (BCrypt.Net.BCrypt.Verify(userAuthenticateRequest.Password, storedHash) == true)
                //{
                    return Ok(filterUser);
                //}
            } else
            {
                //Console.WriteLine("NOT FOUND");
                //Console.ReadLine();
                return NotFound();
            }
        }
    }
}
