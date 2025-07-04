using TaskManager.Domain.Entities;

namespace TaskListManager.Application.Services
{
    public interface ITaskService
    {
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(string id);
        Task<bool> DeleteTaskAsync(string id);
    }
}