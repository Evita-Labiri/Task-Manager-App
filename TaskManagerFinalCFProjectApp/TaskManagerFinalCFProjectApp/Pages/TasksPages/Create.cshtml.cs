using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp.Pages.TasksPages
{
    public class CreateModel : PageModel
    {        
        private readonly ITaskService? taskService;
        private readonly ICategoryService? categoryService;

        public TaskDTO TaskDto { get; set; } = new();
        public List<Category>? Categories { get; set; } = new();
        public List<TasksStatus>? TaskStatuses { get; set; } = new();

        public CreateModel(ITaskService? taskService, ICategoryService? categoryService)
        {
            this.taskService = taskService;
            this.categoryService = categoryService;
        }

        public void OnGet()
        {
            Categories = categoryService?.GetAllCategories();
            TaskStatuses = Enum.GetValues(typeof(TasksStatus)).Cast<TasksStatus>().ToList();
        }

        public void OnPost()
        {
            try
            {
                TaskDto!.TaskName = Request.Form["taskName"];
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

                taskService!.InsertTask(TaskDto!, categoryDTO!);
                Response.Redirect("/TasksPages/Index");
            }
            catch (InvalidInputException e) 
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }
       
    }
    
}
