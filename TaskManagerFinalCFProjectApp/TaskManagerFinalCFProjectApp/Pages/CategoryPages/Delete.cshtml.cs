using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp.Pages.CategoryPages
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService? categoryService;

        public DeleteModel(ICategoryService? categoryService)
        {
            this.categoryService = categoryService;
        }

        public void OnGet()
        {
            try
            {
                TempData["mydata"] = "tempData";
                int id = int.Parse(Request.Query["id"]);
                Category? category = categoryService!.GetCategoryById(id);

                if (category == null)
                {
                    Response.Redirect("/Error");
                }
                else
                {
                    Category? deletedCategory = categoryService!.DeleteCategory(id);

                    if (deletedCategory == null)
                    {
                        TempData["ErrorMessage"] = "Failed to delete the category. Please try again.";
                        Response.Redirect("/Error");
                    }
                    else
                    {
                        Response.Redirect("/CategoryPages/Index");
                    }
                }
            }
            catch (CategoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }

        }     

    }
}

