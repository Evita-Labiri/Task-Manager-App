using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.Service.CategoryService
{
    public interface ICategoryService
    {
        void InsertCategory(CategoryDTO categoryDto);
        void UpdateCategory(CategoryDTO categoryDto);
        Category? DeleteCategory(int id);
        Category? GetCategoryById(int id);
        int GetCategoryIdByName(string categoryName);
        List<Category> GetAllCategories();
        public bool CategoryHasTasks(string categoryName);
    }
}
