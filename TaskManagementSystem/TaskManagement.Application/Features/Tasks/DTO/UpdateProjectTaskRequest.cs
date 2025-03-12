using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Features.Tasks.DTO
{
    public class UpdateProjectTaskRequest
    {
        public Guid TaskId { get; set; }
        public UpdateProjectTaskDto UpdateTaskDto { get; set; }
    }
}
