using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.DAO.CategoryDAO
{
    public interface ICategoryDAO
    {
        void Insert(Category? category);
        void Update(Category? category);
        void Delete(int id);
        Category? GetById(int id);
        Category GetByName(string categoryName);
        List<Category> GetAll();
    }
}
