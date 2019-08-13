namespace TaskTrackingSystem.DAL.DbContext
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using TaskTrackingSystem.DAL.Models;

    public class TaskTrackingSystemDbInitializer : DropCreateDatabaseAlways<TaskTrackingSystemContext>
    {
        protected override void Seed(TaskTrackingSystemContext db)
        {
            var userInitManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleInitManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var initUser = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            userInitManager.Create(initUser, "qwerty12");

            roleInitManager.Create(new IdentityRole("admin"));

            userInitManager.AddToRole(userInitManager.FindByEmail("admin@gmail.com").Id, "admin");

            db.Projects.Add(new Project { Name = "Project1", Description = "Project1 description" });
            db.Projects.Add(new Project { Name = "Project2", Description = "Project2 description" });

            db.SaveChanges();

            db.Positions.Add(new Position { Name = "Position1", IdProject = 1, IdUser = userInitManager.FindByEmail("admin@gmail.com").Id });
            db.Positions.Add(new Position { Name = "Position1", IdProject = 2, IdUser = userInitManager.FindByEmail("admin@gmail.com").Id });

            db.WorkTasks.Add(new WorkTask { Subject = "Test subject 1", IdPerformer = 1, IdProject = 1 });

            base.Seed(db);
        }
    }
}
