using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Features.Departments;
using TaskManagement.Application.Features.Tasks;
using TaskManagement.Application.Features.Tasks.DTO;
using TaskManagement.Domain.Enums;

namespace TaskManagement.WebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IDepartmentService _departmentService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IValidator<CreateProjectTaskDto> _createTaskValidator;
        private readonly IValidator<UpdateProjectTaskDto> _updateTaskValidator;


        public TasksController(ITaskService taskService, IHttpClientFactory httpClient, 
            IDepartmentService departmentService, IValidator<CreateProjectTaskDto> createValidator,
            IValidator<UpdateProjectTaskDto> updateValidator)
        {
            _taskService = taskService;
            _httpClientFactory = httpClient;
            _departmentService = departmentService;
            _createTaskValidator = createValidator;
            _updateTaskValidator = updateValidator;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }


        // REMEMBER TO ADD VALIDATION
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectTaskDto taskDto)
        {
            var validationResult = await _createTaskValidator.ValidateAsync(taskDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var departments = await _departmentService.GetAllDepartmentsAsync();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");

                return View(taskDto); 
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid taskId, TaskState newStatus)
        {
            var validationResult = await _updateTaskValidator.ValidateAsync(taskDto);

            if (!validationResult.IsValid) return View(taskDto);

            var task = await _taskService.GetTaskByIdAsync(taskId);
            if(task == null)
            {
                return NotFound();
            }

            await _taskService.UpdateTaskAsync(task.Id, task.Title, task.Description, newStatus);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.DeleteAsync($"api/tasks/{taskId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete task");
                return View();
            }
        }
    }
}
