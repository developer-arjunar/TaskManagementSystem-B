using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TasksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var allTasks = dbContext.Tasks.ToList();

            //var allTasks = dbContext.Tasks.Include(t => t.Comments).ToList();

            return Ok(allTasks);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTaskById(Guid id)
        {
            //var task = dbContext.Tasks.Find(id);

            var task = dbContext.Tasks.Include(t => t.Comments).FirstOrDefault(t => t.Id == id);

            if (task is null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskRequest saveRequest)
        {
            var task = new Models.Entities.Task()
            {
                Name = saveRequest.Name,
                Description = saveRequest.Description,
                AssigneeId = saveRequest.AssigneeId,
                CreatedBy = "ADMIN",
                CreatedDate = DateTime.Now,
                DueDate = saveRequest.DueDate,
                UpdatedBy = "ADMIN",
                UpdatedDate = DateTime.Now,
                Status = "OPENED"
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            return Ok(task);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateTask(Guid id, UpdateTaskRequest updateRequest)
        {
            var task = dbContext.Tasks.Find(id);

            if (task is null)
            {
                return NotFound();
            }

            task.Name = updateRequest.Name;
            task.Description = updateRequest.Description;
            task.DueDate = updateRequest.DueDate;
            task.UpdatedDate = DateTime.Now;
            task.Status = updateRequest.Status;

            dbContext.SaveChanges();

            return Ok(task);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteTask(Guid id)
        {
            var task = dbContext.Tasks.Find(id);

            if (task is null)
            {
                return NotFound();
            }

            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
