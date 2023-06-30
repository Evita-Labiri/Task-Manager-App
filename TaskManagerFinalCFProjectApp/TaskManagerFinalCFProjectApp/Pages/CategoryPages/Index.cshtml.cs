using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerFinalCFProjectApp.DAO.CategoryDAO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.CategoryService;

namespace TaskManagerFinalCFProjectApp.Pages.CategoryPages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService? categoryService;
        private readonly ICategoryDAO categoryDAO = new CategoryDAOImpl();

        public List<Category>? categories = new();

        public IndexModel()
        {
            categoryService = new CategoryServiceImpl(categoryDAO);
        }

        public void OnGet()
        {
            try
            {
               categories = categoryService!.GetAllCategories();

            }
            catch (CategoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Response.Redirect("/Error");
            }
        }
    }
}
