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
                Status = user.Status,
                JoinedDate = user.JoinedDate
            }).ToList();

            return Ok(getAllResponse);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var getResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Username = user.Username.ToUpper(),
                Status = user.Status,
                JoinedDate = user.JoinedDate,
                Role = user.Role
            };

            return Ok(getResponse);
        }

        [HttpGet]
        [Route("checkUsernameIsExists/{username}")]
        public IActionResult CheckUsernameIsExists(String username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username is required");
            }

            bool isExists = dbContext.Users.Any(u => u.Username.ToUpper() == username.ToUpper());
            return Ok(isExists);
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
                JoinedDate = DateTime.Now,
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
                JoinedDate = savedUser.JoinedDate,
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
                        JoinedDate = filterUser.JoinedDate,
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

        //[HttpPut]
        //[Route("{id:guid}")]
        //public IActionResult UpdateUser(Guid id, UpdateTaskRequest updateRequest)
        //{
        //    var task = dbContext.Tasks.Find(id);

        //    if (task is null)
        //    {
        //        return NotFound();
        //    }

        //    task.Name = updateRequest.Name;
        //    task.Description = updateRequest.Description;
        //    task.DueDate = updateRequest.DueDate;
        //    task.UpdatedBy = updateRequest.UpdatedBy;
        //    task.UpdatedDate = DateTime.Now;
        //    task.Status = updateRequest.Status;
        //    task.AssigneeId = updateRequest.AssigneeId;

        //    dbContext.SaveChanges();

        //    var updatedTask = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.Id == task.Id);

        //    if (updatedTask is null)
        //    {
        //        return BadRequest();
        //    }

        //    var updateResponse = new AddTaskResponse()
        //    {
        //        Id = updatedTask.Id,
        //        Name = updatedTask.Name,
        //        Description = updatedTask.Description,
        //        CreatedBy = updatedTask.CreatedBy,
        //        CreatedDate = updatedTask.CreatedDate,
        //        DueDate = updatedTask.DueDate,
        //        UpdatedBy = updatedTask.UpdatedBy,
        //        UpdatedDate = updatedTask.UpdatedDate,
        //        Status = updatedTask.Status,
        //        Assignee = new UserSimpleResponse()
        //        {
        //            Id = updatedTask.Assignee.Id,
        //            Name = updatedTask.Assignee.Name,
        //            Email = updatedTask.Assignee.Email,
        //            PhoneNo = task.Assignee.PhoneNo,
        //            Status = task.Assignee.Status,
        //            JoinedDate = task.Assignee.JoinedDate
        //        }
        //    };

        //    return Ok(updateResponse);
        //}

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
