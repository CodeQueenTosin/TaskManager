using Newtonsoft.Json;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;

namespace YourNamespace.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://68655ded5b5d8d033980e6ad.mockapi.io/api/v1/Task";

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Converters = { new UnixMillisecondsDateTimeConverter() },
            NullValueHandling = NullValueHandling.Ignore
        };

        public TaskRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task, _jsonSettings), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TaskItem>(responseContent, _jsonSettings);
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TaskItem>>(content, _jsonSettings);
        }

        public async Task<TaskItem> GetTaskByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TaskItem>(content, _jsonSettings);
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
