namespace TaskTrackingSystem.BLL.DTO
{
    using System;
    using System.Collections.Generic;
    using TaskTrackingSystem.DAL.Models;

    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Position> ProjectPositions { get; set; }

        public virtual ICollection<WorkTask> ProjectTasks { get; set; }
    }
}
