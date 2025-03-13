using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.DTO
{
    public class UpdateProjectTaskDto : BaseProjectTaskDto
    {
        public Guid TaskId { get; set; }
        public TaskState Status { get; set; }

        public UpdateProjectTaskDto()
        {
        }
    }
}
