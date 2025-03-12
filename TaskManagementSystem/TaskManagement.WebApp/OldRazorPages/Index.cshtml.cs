using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.Features.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.WebApp.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(ITaskService taskService, IHttpClientFactory httpClient)
        {
            _taskService = taskService;
            _httpClientFactory = httpClient;
        }

        public List<ProjectTask> Tasks { get; set; } = new();
        [BindProperty] public ProjectTask NewTask { get; set; } = new();
        public List<TaskState> StatusOptions { get; } = Enum.GetValues(typeof(TaskState)).Cast<TaskState>().ToList();

        public async Task OnGetAsync()
        {
            Tasks = (await _taskService.GetAllTasksAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpClientFactory.CreateClient("API");

            var response = await client.PostAsJsonAsync("api/Tasks", NewTask);
            if (!response.IsSuccessStatusCode) return Page();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(Guid taskId, TaskState newStatus)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null) return NotFound();

            await _taskService.UpdateTaskAsync(task.Id, task.Title, task.Description, newStatus);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);
            return RedirectToPage();
        }
    }
}
