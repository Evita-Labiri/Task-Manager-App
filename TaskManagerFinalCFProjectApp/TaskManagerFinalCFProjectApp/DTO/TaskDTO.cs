using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int CategoryId { get; set; }
        public TasksStatus Status { get; set; }
    }
}
