using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.DAO.TaskDAO
{
    public interface ITaskDAO
    {
        void Insert(Tasks? task, string? categoryName);
        void Update(Tasks? task, string? categoryName);
        void Delete(int id);
        Tasks? GetById(int id);
        List<Tasks> GetByCategoryName(string categoryName);
        List<Tasks> GetAll();
    }
}
