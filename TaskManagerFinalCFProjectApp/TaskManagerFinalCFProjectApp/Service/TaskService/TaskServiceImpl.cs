using TaskManagerFinalCFProjectApp.DAO.TaskDAO;
using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Exceptions;
using TaskManagerFinalCFProjectApp.Model;
using TaskStatus = TaskManagerFinalCFProjectApp.Model.TasksStatus;

namespace TaskManagerFinalCFProjectApp.Service.TaskService
{
    public class TaskServiceImpl : ITaskService
    {
        private readonly ITaskDAO? _taskDAO;

        public TaskServiceImpl()
        {
        }

        public TaskServiceImpl(ITaskDAO taskDAO)
        {
            _taskDAO = taskDAO;
        }

        public void InsertTask(TaskDTO? taskDto, CategoryDTO categoryDto)
        {
            if (taskDto == null) return;

            Tasks? task = MapTask(taskDto);
            Category? category = MapCategory(categoryDto);
            try
            {
                _taskDAO?.Insert(task, category?.CategoryName);
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void UpdateTask(TaskDTO? taskDto, CategoryDTO? categoryDto)
        {
            if (taskDto == null) return;

            Tasks? task = MapTask(taskDto);
            Category? category = MapCategory(categoryDto!);
            try
            {
                _taskDAO?.Update(task, category?.CategoryName);
            }
            catch (TaskNotFoundException e)
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

        public Tasks? DeleteTask(int id)
        {
            Tasks? task = null;

            try
            {
                task = _taskDAO?.GetById(id);
                if (task == null) return null;
                _taskDAO?.Delete(id);
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }

            return task;
        }

        public Tasks? GetTaskById(int id)
        {
            try
            {
                return _taskDAO?.GetById(id);
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public List<Tasks>? GetTasksByCategory(string categoryName)
        {
            try
            {
                return _taskDAO?.GetByCategoryName(categoryName);
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public List<Tasks>? GetAllTasks()
        {
            try
            {
                return _taskDAO?.GetAll();
            }
            catch (TaskNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new TaskNotFoundException("Error: Tasks were not found.");
            }
        }

        public bool IsCompleted(TaskDTO taskDto)
        {
            try
            {
                return taskDto.Status == TaskStatus.Completed;

            }
            catch (StatusNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public bool IsPending(TaskDTO taskDto)
        {
            try
            {
                return taskDto.Status == TaskStatus.Pending;
            }
            catch (StatusNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public bool IsInProgress(TaskDTO taskDto)
        {
            try
            {
                return taskDto.Status == TaskStatus.InProgress;
            }
            catch (StatusNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public bool IsExpired(TaskDTO taskDto)
        {
            try
            {
                if (taskDto.DueDate.HasValue)
                {
                    return taskDto.DueDate < DateTime.Now;
                }

                return false;

            }
            catch (DateNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        private Tasks? MapTask(TaskDTO? taskDto)
        {
            if (taskDto == null) return null;
            Tasks? task = new Tasks()
            {
                Id = taskDto.Id,
                TaskName = taskDto.TaskName,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                CategoryId = taskDto.CategoryId,
                Status = taskDto.Status
            };

            return task;
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
