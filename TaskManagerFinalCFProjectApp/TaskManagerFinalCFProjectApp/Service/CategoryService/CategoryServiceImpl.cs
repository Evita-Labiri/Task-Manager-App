using System.Data.SqlClient;
using TaskManagerFinalCFProjectApp.DAO.CategoryDAO;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskManagerFinalCFProjectApp.Service.TaskService;


namespace TaskManagerFinalCFProjectApp.Service.CategoryService
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly ICategoryDAO? categoryDAO;
        private readonly ITaskService? taskService = new TaskServiceImpl();

        public CategoryServiceImpl()
        {
        }

        public CategoryServiceImpl(ICategoryDAO? categoryDAO)
        {
            this.categoryDAO = categoryDAO;

    }

    public void InsertCategory(CategoryDTO categoryDto)
        {
            if (categoryDto == null) return;

            Category? category = MapCategory(categoryDto);
            try
            {
                categoryDAO?.Insert(category);
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void UpdateCategory(CategoryDTO categoryDto)
        {
            if (categoryDto == null) return;

            Category? category = MapCategory(categoryDto);
            try
            {
                categoryDAO?.Update(category);
            }
            catch (CategoryNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
            catch (InvalidInputException e1)
            {
                Console.WriteLine(e1.StackTrace);
                throw;
            }
        }

        public Category? DeleteCategory(int id)
        {
            Category? category = null;

            try
            {
                category = categoryDAO?.GetById(id);
                string? categoryName = category!.CategoryName;
                if (CategoryHasTasks(categoryName!))
                {
                    return null;
                }
                categoryDAO?.Delete(id);
                if (category == null) return null;

            }
            catch (CategoryHasTasksException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
            catch (CategoryNotFoundException e1)
            {
                Console.WriteLine(e1.StackTrace);
                throw;
            }
            catch (SqlException)
            {
                return null;
            }

            return category;
        }

        public Category? GetCategoryById(int id)
        {
            try
            {
                return categoryDAO?.GetById(id);          
            }
            catch (CategoryNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public int GetCategoryIdByName(string categoryName)
        {
            var categories = GetAllCategories();
            foreach (var category in categories)
            {
                if (category.CategoryName == categoryName)
                {
                    return category.Id;
                }
            }
            return 0; 
        }


        public List<Category> GetAllCategories()
        {
            try
            {
                return categoryDAO!.GetAll();
            }
            catch (CategoryNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new CategoryNotFoundException("Error: Categories not found");
            }
        }

        public bool CategoryHasTasks(string categoryName)
        {
            List<Tasks> tasks = taskService!.GetTasksByCategory(categoryName)!;
            return tasks?.Count > 0;
        }


        private Category? MapCategory(CategoryDTO categoryDto)
        {
            if (categoryDto == null) return null;
            Category? category = new Category()
            {
                Id = categoryDto.Id,
                CategoryName = categoryDto.CategoryName
            };

            return category;
        }

        
    }
}
