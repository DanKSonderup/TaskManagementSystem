using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagement.Application.Features.Tasks.DTO;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks.Validators
{
    public class UpdateTaskValidator: BaseProjectTaskDtoValidator<UpdateProjectTaskDto>
    {
        
        public UpdateTaskValidator() 
        { 
            RuleFor(x => x.Status).NotNull().WithMessage("Status is required");

            RuleFor(x => x.Department).NotNull().WithMessage("Department is required");

            When(x => (x.Department?.Name ?? string.Empty) != "IT", () => {
                RuleFor(x => x.Status)
                    .NotEqual(TaskState.CodeReview)
                    .WithMessage("CodeReview is only allowed for the IT department.");
            });
        }
    }
}
