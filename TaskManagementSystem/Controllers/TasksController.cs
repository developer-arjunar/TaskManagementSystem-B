using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.DTOs.Responses;
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
            var tasks = dbContext.Tasks.Include(t => t.Comments).Include(t => t.Assignee).ToList();

            if (tasks is null)
            {
                return NotFound();
            }

            var getAllResponse = tasks.Select(task => new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UpdatedBy = task.UpdatedBy,
                UpdatedDate = task.UpdatedDate,
                Status = task.Status,
                Comments = task.Comments,
                Assignee = new UserSimpleResponse()
                {
                    Id = task.Assignee.Id,
                    Name = task.Assignee.Name,
                    Email = task.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            }).ToList();

            return Ok(getAllResponse);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTaskById(Guid id)
        {
            var task = dbContext.Tasks.Include(t => t.Comments).Include(t => t.Assignee).FirstOrDefault(t => t.Id == id);

            if (task is null)
            {
                return NotFound();
            }

            var getResponse = new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UpdatedBy = task.UpdatedBy,
                UpdatedDate = task.UpdatedDate,
                Status = task.Status,
                Comments = task.Comments,
                Assignee = new UserSimpleResponse()
                {
                    Id = task.Assignee.Id,
                    Name = task.Assignee.Name,
                    Email = task.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            };

            return Ok(getResponse);
        }

        [HttpGet]
        [Route("byAssigneeId/{assigneeId:guid}")]
        public IActionResult GetAllTasksByAssigneeId(Guid assigneeId)
        {
            var allTasksByAssigneeId = dbContext.Tasks.Include(t => t.Comments).Include(t => t.Assignee).Where(t => t.AssigneeId == assigneeId).ToList();

            if (allTasksByAssigneeId is null)
            {
                return NotFound();
            }

            var getAllByAssigneeIdResponse = allTasksByAssigneeId.Select(task => new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UpdatedBy = task.UpdatedBy,
                UpdatedDate = task.UpdatedDate,
                Status = task.Status,
                Comments = task.Comments,
                Assignee = new UserSimpleResponse()
                {
                    Id = task.Assignee.Id,
                    Name = task.Assignee.Name,
                    Email = task.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            }).ToList();

            return Ok(getAllByAssigneeIdResponse);
        }

        [HttpGet]
        [Route("byAssigneeIdAndStatus/{assigneeId:guid}/{status}")]
        public IActionResult GetTasksByStatus(Guid assigneeId, String status)
        {
            var allTasksByAssigneeIdAndStatus = dbContext.Tasks.Include(t => t.Comments).Include(t => t.Assignee).Where(t => t.AssigneeId == assigneeId && t.Status == status).ToList();

            if (allTasksByAssigneeIdAndStatus is null)
            {
                return NotFound();
            }

            var getAllByAssigneeIdAndStatusResponse = allTasksByAssigneeIdAndStatus.Select(task => new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UpdatedBy = task.UpdatedBy,
                UpdatedDate = task.UpdatedDate,
                Status = task.Status,
                Comments = task.Comments,
                Assignee = new UserSimpleResponse()
                {
                    Id = task.Assignee.Id,
                    Name = task.Assignee.Name,
                    Email = task.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            }).ToList();

            return Ok(getAllByAssigneeIdAndStatusResponse);
        }

        [HttpGet]
        [Route("byStatus/{status}")]
        public IActionResult GetTasksByStatus(String status)
        {
            var allTasksByStatus = dbContext.Tasks.Include(t => t.Comments).Include(t => t.Assignee).Where(t => t.Status == status).ToList();

            if (allTasksByStatus is null)
            {
                return NotFound();
            }

            var getAllByStatusResponse = allTasksByStatus.Select(task => new GetTaskResponse()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UpdatedBy = task.UpdatedBy,
                UpdatedDate = task.UpdatedDate,
                Status = task.Status,
                Comments = task.Comments,
                Assignee = new UserSimpleResponse()
                {
                    Id = task.Assignee.Id,
                    Name = task.Assignee.Name,
                    Email = task.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            }).ToList();

            return Ok(getAllByStatusResponse);
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskRequest saveRequest)
        {
            var task = new Models.Entities.Task()
            {
                Name = saveRequest.Name,
                Description = saveRequest.Description,
                AssigneeId = saveRequest.AssigneeId,
                CreatedBy = saveRequest.CreatedBy,
                CreatedDate = DateTime.Now,
                DueDate = saveRequest.DueDate,
                UpdatedBy = saveRequest.UpdatedBy,
                UpdatedDate = DateTime.Now,
                Status = saveRequest.Status
            };

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            var savedTask = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.Id == task.Id);

            if (savedTask is null)
            {
                return BadRequest();
            }

            var saveResponse = new AddTaskResponse()
            {
                Id = savedTask.Id,
                Name = savedTask.Name,
                Description = savedTask.Description,
                CreatedBy = savedTask.CreatedBy,
                CreatedDate = savedTask.CreatedDate,
                DueDate = savedTask.DueDate,
                UpdatedBy = savedTask.UpdatedBy,
                UpdatedDate = savedTask.UpdatedDate,
                Status = savedTask.Status,
                Assignee = new UserSimpleResponse()
                {
                    Id = savedTask.Assignee.Id,
                    Name = savedTask.Assignee.Name,
                    Email = savedTask.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            };

            return Ok(saveResponse);
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
            task.UpdatedBy = updateRequest.UpdatedBy;
            task.UpdatedDate = DateTime.Now;
            task.Status = updateRequest.Status;
            task.AssigneeId = updateRequest.AssigneeId;

            dbContext.SaveChanges();

            var updatedTask = dbContext.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.Id == task.Id);

            if (updatedTask is null)
            {
                return BadRequest();
            }

            var updateResponse = new AddTaskResponse()
            {
                Id = updatedTask.Id,
                Name = updatedTask.Name,
                Description = updatedTask.Description,
                CreatedBy = updatedTask.CreatedBy,
                CreatedDate = updatedTask.CreatedDate,
                DueDate = updatedTask.DueDate,
                UpdatedBy = updatedTask.UpdatedBy,
                UpdatedDate = updatedTask.UpdatedDate,
                Status = updatedTask.Status,
                Assignee = new UserSimpleResponse()
                {
                    Id = updatedTask.Assignee.Id,
                    Name = updatedTask.Assignee.Name,
                    Email = updatedTask.Assignee.Email,
                    PhoneNo = task.Assignee.PhoneNo,
                    Status = task.Assignee.Status
                }
            };

            return Ok(updateResponse);
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
