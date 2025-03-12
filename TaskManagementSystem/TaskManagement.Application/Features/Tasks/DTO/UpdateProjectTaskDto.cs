using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.DTO
{
    public class UpdateProjectTaskDto : BaseProjectTaskDto
    {
        public TaskState Status { get; set; }
    }
}
