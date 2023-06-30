using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;

namespace TaskManagerFinalCFProjectApp.Pages.CategoryPages
{
    public class UpdateModel : PageModel
    {
        private readonly ICategoryService? categoryService;
        public CategoryDTO CategoryDto { get; set; } = new();


        public UpdateModel(ICategoryService? categoryService)
        {
            this.categoryService = categoryService;
        }


        public void OnGet()
        {
            try
            {
                Category? category;
                int id = int.Parse(Request.Query["id"]);
                category = categoryService!.GetCategoryById(id);

                CategoryDto = MapCategoryToDto(category)!;
            }
            catch(CategoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }

        public void OnPost()
        {
            try
            {
                CategoryDto.Id = int.Parse(Request.Form["id"]);
                CategoryDto.CategoryName = Request.Form["categoryName"];

                categoryService!.UpdateCategory(CategoryDto);
                Response.Redirect("/CategoryPages/Index");
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }

        private CategoryDTO? MapCategoryToDto(Category? category)
        {
            CategoryDTO? categoryDto = new CategoryDTO();

            categoryDto.Id = category!.Id;
            categoryDto.CategoryName = category.CategoryName;

            return categoryDto;            
        }
    }
}
