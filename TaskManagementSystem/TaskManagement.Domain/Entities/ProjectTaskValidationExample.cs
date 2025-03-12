using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{

    // Example of doing Validation with Annotations instead of Fluent Validation

    /*
    public class ProjectTaskValidationExample
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title must be under 100 characters")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public TaskState State { get; private set; }

        public ProjectTaskValidationExample()
        {
            Id = Guid.NewGuid();
            State = TaskState.ToDo;
        }
        public ProjectTaskValidationExample(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            State = TaskState.ToDo;
        }
    } */
}
