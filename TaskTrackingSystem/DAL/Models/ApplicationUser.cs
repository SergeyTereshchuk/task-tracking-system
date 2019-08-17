namespace TaskTrackingSystem.DAL.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Position> UserPositions { get; set; }
    }
}
