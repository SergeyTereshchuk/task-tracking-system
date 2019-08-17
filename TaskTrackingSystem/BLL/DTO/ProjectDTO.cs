namespace TaskTrackingSystem.BLL.DTO
{
    using System;

    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public string Description { get; set; }
    }
}
