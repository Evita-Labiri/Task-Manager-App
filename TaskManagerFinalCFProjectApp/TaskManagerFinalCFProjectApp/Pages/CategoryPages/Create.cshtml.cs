using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Service.CategoryService;

namespace TaskManagerFinalCFProjectApp.Pages.CategoryPages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService? categoryService;
        public CategoryDTO? CategoryDto { get; set; } = new();


        public CreateModel(ICategoryService? categoryService)
        {
            this.categoryService = categoryService;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            try
            {
                CategoryDto!.CategoryName = Request.Form["categoryName"];

                categoryService!.InsertCategory(CategoryDto);
                Response.Redirect("/CategoryPages/Index");
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }
    }
}
