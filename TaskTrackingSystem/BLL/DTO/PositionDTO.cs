namespace TaskTrackingSystem.BLL.DTO
{
    using System.Collections.Generic;
    using TaskTrackingSystem.DAL.Models;

    public class PositionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IdUser { get; set; }

        public int IdProject { get; set; }

        public ApplicationUser PositionUser { get; set; }

        public Project PositionProject { get; set; }

        public ICollection<WorkTask> PositionTasks { get; set; }
    }
}
