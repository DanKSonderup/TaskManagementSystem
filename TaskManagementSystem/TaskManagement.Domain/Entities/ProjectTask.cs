using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public TaskState State { get; private set; }

        public ProjectTask() 
        {
            Id = Guid.NewGuid();
            State = TaskState.ToDo;
        }
        public ProjectTask(string title, string description, Department department)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            State = TaskState.ToDo;
            Department = department;
        }

        public void MarkAsCompleted() => State = TaskState.Done;

        public void Update(string title, string description, TaskState status)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Title = title;
            Description = description;
            State = status;
        }
    }
}
