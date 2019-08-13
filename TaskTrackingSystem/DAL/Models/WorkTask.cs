namespace TaskTrackingSystem.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class WorkTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        public int IdProject { get; set; }

        public int IdPerformer { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual Position TaskPerformer { get; set; }

        public virtual Project TaskProject { get; set; }
    }
}
