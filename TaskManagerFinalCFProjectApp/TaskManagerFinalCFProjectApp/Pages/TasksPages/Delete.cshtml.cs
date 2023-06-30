
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp.Pages.TasksPages
{
    public class DeleteModel : PageModel
    {
        private readonly ITaskService? taskService;

        public DeleteModel(ITaskService? taskService)
        {
            this.taskService = taskService;

        }

        public void OnGet(int id)
        {
            try
            {
                TempData["mydata"] = "tempData";
                Tasks? task = taskService!.GetTaskById(id);

                string confirm = Request.Query["confirm"];

                if (task == null)
                {
                    RedirectToPage("/Error");
                }


                if (confirm != "true")
                {
                    RedirectToPage("/TasksPages/Index");
                }

                taskService.DeleteTask(id);
                Response.Redirect("/TasksPages/Index");
            }
            catch (TaskNotFoundException e1)
            {
                Console.WriteLine(e1.Message);
                RedirectToPage("/Error");
            }
        }
    }
}
