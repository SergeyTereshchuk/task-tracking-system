namespace TaskTrackingSystem.DAL.Repositories
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.DAL.Models;

    public class UserRepository : UserStore<ApplicationUser>
    {
    }
}
