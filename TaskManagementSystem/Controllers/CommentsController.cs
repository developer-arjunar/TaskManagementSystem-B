using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CommentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("byTaskId/{taskId:guid}")]
        public IActionResult GetAllCommentsByTaskId(Guid taskId)
        {
            var allCommentsByTaskId = dbContext.Comments.Where(c => c.TaskId == taskId).ToList();

            return Ok(allCommentsByTaskId);
        }

        [HttpPost]
        public IActionResult AddComment(AddCommentRequest saveRequest)
        {
            var comment = new Comment()
            {
                TaskComment = saveRequest.TaskComment,
                CreatedDate = DateTime.Now,
                TaskId = saveRequest.TaskId
            };

            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

            return Ok(comment);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateComment(Guid id, UpdateCommentRequest updateRequest)
        {
            var comment = dbContext.Comments.Find(id);

            if (comment is null)
            {
                return NotFound();
            }

            comment.TaskComment = updateRequest.TaskComment;

            dbContext.SaveChanges();

            return Ok(comment);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteComment(Guid id) 
        {
            var comment = dbContext.Comments.Find(id);

            if (comment is null)
            {
                return NotFound();
            }

            dbContext.Comments.Remove(comment);
            dbContext.SaveChanges();

            return Ok();
        }

    }
}
