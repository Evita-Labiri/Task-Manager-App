using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp.Pages.TasksPages
{


    public class TasksByCategoryModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;

        public List<Tasks>? FilteredTasks { get; set; }

        public TasksByCategoryModel(ITaskService taskService, ICategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;

        }

        public void OnGet(string categoryName)
        {
            try
            {
                FilteredTasks = _taskService.GetTasksByCategory(categoryName);
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }
    }
}
