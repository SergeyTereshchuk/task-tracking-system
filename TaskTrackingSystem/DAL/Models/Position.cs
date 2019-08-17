namespace TaskTrackingSystem.DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Position
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IdUser { get; set; }

        [Required]
        public int IdProject { get; set; }

        public virtual ApplicationUser PositionUser { get; set; }

        public virtual Project PositionProject { get; set; }

        public virtual ICollection<WorkTask> PositionTasks { get; set; }
    }
}
