using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Features.Tasks;
using TaskManagement.Application.Features.Tasks.DTO;
using TaskManagement.Domain.Entities;

namespace TaskManagement.WebApp.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskAPI : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IValidator<CreateProjectTaskDto> _createTaskValidator;

        public TaskAPI(ITaskService taskService, IValidator<CreateProjectTaskDto> validator)
        {
            _taskService = taskService;
            _createTaskValidator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectTaskDto dto)
        {
            ValidationResult result = _createTaskValidator.Validate(dto);

            if(!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var taskId = await _taskService.CreateTaskAsync(dto.Title, dto.Description, dto.Department ?? new Department { Id = Guid.NewGuid(), Name = "Unknown"});
            return CreatedAtAction(nameof(GetById), new { id = taskId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectTaskDto dto)
        {
            var result = await _taskService.UpdateTaskAsync(id, dto.Title, dto.Description, dto.Status);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
