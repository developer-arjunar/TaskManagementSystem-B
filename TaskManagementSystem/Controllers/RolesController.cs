using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.DTOs.Requests;
using TaskManagementSystem.Models.Entities;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RolesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var allRoles = dbContext.Roles.ToList();

            return Ok(allRoles);
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleRequest saveRequest)
        {
            var role = new Role()
            {
                Name = saveRequest.Name,
                Description = saveRequest.Description
            };

            dbContext.Roles.Add(role);
            dbContext.SaveChanges();

            return Ok(role);
        }
    }
}
