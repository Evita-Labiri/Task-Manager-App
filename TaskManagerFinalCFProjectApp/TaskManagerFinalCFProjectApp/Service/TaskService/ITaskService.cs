using TaskManagerFinalCFProjectApp.DTO;
using TaskManagerFinalCFProjectApp.Model;

namespace TaskManagerFinalCFProjectApp.Service.TaskService
{
    public interface ITaskService
    {
        void InsertTask(TaskDTO taskDto, CategoryDTO categoryDto);
        void UpdateTask(TaskDTO taskDto, CategoryDTO categryDto);
        Tasks? DeleteTask(int id);
        Tasks? GetTaskById(int id);
        List<Tasks>? GetTasksByCategory(string categoryName);
        List<Tasks>? GetAllTasks();
        bool IsExpired(TaskDTO taskDto);
        bool IsCompleted(TaskDTO taskDto);
        bool IsPending(TaskDTO taskDto);
        bool IsInProgress(TaskDTO taskDto);
    }
}
