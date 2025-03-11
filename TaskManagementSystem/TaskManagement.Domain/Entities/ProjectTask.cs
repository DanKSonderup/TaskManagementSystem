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
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskState State { get; private set; }

        public ProjectTask() 
        {
            Id = Guid.NewGuid();
            State = TaskState.ToDo;
        }
        public ProjectTask(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            State = TaskState.ToDo;
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
