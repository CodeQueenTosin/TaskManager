using TaskManager.Domain.Entities;
namespace TaskManager.Infrastructure
{
    public interface ITaskRepository
    {
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(string id);
        Task<bool> DeleteTaskAsync(string id);
    }

}
