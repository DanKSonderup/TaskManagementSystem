using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.WebApp.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;

        public IndexModel(ITaskService taskService)
        {
            _taskService = taskService;
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

            await _taskService.CreateTaskAsync(NewTask.Title, NewTask.Description);
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
