namespace TaskTrackingSystem.BLL.DTO
{
    using System;
    using TaskTrackingSystem.DAL.Models;

    public class WorkTaskDTO
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public int IdProject { get; set; }

        public int IdPerformer { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public virtual Position TaskPerformer { get; set; }

        public virtual Project TaskProject { get; set; }
    }
}
