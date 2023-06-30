using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.Service.TaskService;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Exceptions;

namespace TaskManagerFinalCFProjectApp.Pages.TasksPages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService? taskService = new TaskServiceImpl();
        private readonly ICategoryService? categoryService = new CategoryServiceImpl();

        public List<Tasks>? tasks = new();
        public List<Category>? categories = new();

        public IndexModel(ITaskService? taskService, ICategoryService? categoryService)
        {
            this.taskService = taskService;
            this.categoryService = categoryService;
        }

        public void OnGet()
        {
            try
            {
                tasks = taskService!.GetAllTasks();
                categories = categoryService!.GetAllCategories();

            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }

        public string GetTaskStatus(Tasks task)
        {
            TaskDTO taskDto = MapTaskToDto(task);

            if (taskService!.IsCompleted(taskDto))
            {
                return "Completed";
            }
            else if (taskService!.IsPending(taskDto))
            {
                return "Pending";
            }
            else if (taskService!.IsInProgress(taskDto))
            {
                return "In Progress";
            }
            else
            {
                return "Unknown";
            }
        }

        private TaskDTO MapTaskToDto(Tasks task)
        {
            TaskDTO taskDto = new TaskDTO();

            taskDto.Id = task.Id;
            taskDto.TaskName = task.TaskName;
            taskDto.Description = task.Description;
            taskDto.DueDate = task.DueDate;
            taskDto.CategoryId = task.CategoryId;
            taskDto.Status = task.Status;

            return taskDto;
        }
       
    }
}

