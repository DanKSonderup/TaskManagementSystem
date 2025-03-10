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
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskState State { get; private set; }

        public ProjectTask(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            State = TaskState.ToDo;
        }

        public void MarkAsCompleted() => State = TaskState.Done;
    }
}
