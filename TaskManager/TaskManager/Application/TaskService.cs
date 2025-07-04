using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;

namespace TaskListManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            return await _taskRepository.AddTaskAsync(task);
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(string id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
