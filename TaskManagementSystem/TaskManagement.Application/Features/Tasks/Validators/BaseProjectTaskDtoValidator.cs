using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Application.Features.Tasks.DTO;

namespace TaskManagement.Application.Features.Tasks.Validators
{
    public class BaseProjectTaskDtoValidator<T> : AbstractValidator<T> where T : BaseProjectTaskDto
    {
        public BaseProjectTaskDtoValidator() 
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Title).MaximumLength(100).WithMessage("Title must be under 100 characters");
            RuleFor(x => x.Department).NotNull().WithMessage("Department is required");
        }
    }
}
