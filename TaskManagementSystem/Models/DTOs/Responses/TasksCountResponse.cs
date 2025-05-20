namespace TaskManagementSystem.Models.DTOs.Responses
{
    public class TasksCountResponse
    {

        public required int OpenedTasks { get; set; }

        public required int InProgressTasks { get; set; }

        public required int CompletedTasks { get; set; }

        public required int OverdueTasks { get; set; }
    }
}
