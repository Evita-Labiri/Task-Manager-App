using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp.Pages.TasksPages
{
    public class UpdateModel : PageModel
    {
        private readonly ITaskService? taskService;
        private readonly ICategoryService? categoryService;

        public TaskDTO TaskDto { get; set; } = new();
        public List<Category>? Categories { get; set; }
        public List<TasksStatus>? TaskStatuses { get; set; }

        public UpdateModel(ITaskService? taskService, ICategoryService? categoryService)
        {
            this.taskService = taskService;
            this.categoryService = categoryService;
        }

        public void OnGet()
        {
            try
            {
                Tasks? task;

                int id = int.Parse(Request.Query["id"]);
                task = taskService!.GetTaskById(id);

                TaskDto = MapTaskToDto(task!);

                Categories = categoryService?.GetAllCategories();
                TaskStatuses = Enum.GetValues(typeof(TaskStatus)).Cast<TasksStatus>().ToList();
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }

        public void OnPost()
        {
            try
            {
                TaskDto.Id = int.Parse(Request.Form["id"]);
                TaskDto.TaskName = Request.Form["taskName"];
                TaskDto.Description = Request.Form["description"];
                string dueDateStr = Request.Form["dueDate"];
                if (DateTime.TryParse(dueDateStr, out DateTime dueDate))
                {
                    TaskDto.DueDate = dueDate;
                }
                else
                {
                    Console.WriteLine("Invalid Date Format");
                }
                string categoryValue = Request.Form["category"];
                CategoryDTO? categoryDTO = null;
                if (int.TryParse(categoryValue, out int categoryId))
                {
                    TaskDto.CategoryId = categoryId;
                    categoryDTO = new CategoryDTO
                    {
                        Id = TaskDto.CategoryId,
                        CategoryName = categoryService!.GetCategoryById(TaskDto.CategoryId)?.CategoryName
                    };

                }
                if (Enum.TryParse(Request.Form["status"], out TasksStatus status))
                {
                    TaskDto.Status = status;
                }

                taskService!.UpdateTask(TaskDto, categoryDTO!);
                Categories = categoryService?.GetAllCategories();
                Response.Redirect("/TasksPages/Index");
            }
            catch(InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
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
