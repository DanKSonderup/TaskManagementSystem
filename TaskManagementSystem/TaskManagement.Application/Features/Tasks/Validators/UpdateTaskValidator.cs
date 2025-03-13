using FluentValidation;
using FluentValidation.Results;
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
    public class UpdateTaskValidator: AbstractValidator<UpdateProjectTaskDto>
    {

        public UpdateTaskValidator()
        {
            RuleFor(x => x.Status)
                .NotNull().WithMessage("Status is required");

            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("Task ID is required");

            When(x => (x.DepartmentName ?? string.Empty) != "IT", () =>
            {
                RuleFor(x => x.Status)
                    .NotEqual(TaskState.CodeReview)
                    .WithMessage("CodeReview is only allowed for the IT department.");
            });
        }
    }
}
