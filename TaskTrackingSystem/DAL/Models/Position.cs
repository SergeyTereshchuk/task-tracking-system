namespace TaskTrackingSystem.DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Position
    {
        [Required]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        public string IdUser { get; set; }

        public int IdProject { get; set; }

        public virtual ApplicationUser PositionUser { get; set; }

        public virtual Project PositionProject { get; set; }

        public virtual ICollection<WorkTask> PositionTasks { get; set; }
    }
}
