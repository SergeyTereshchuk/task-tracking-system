namespace TaskTrackingSystem.DAL.DbContext
{
    using System;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.DAL.Models;

    public class TaskTrackingSystemDbInitializer : DropCreateDatabaseAlways<TaskTrackingSystemContext>
    {
        protected override void Seed(TaskTrackingSystemContext db)
        {
            var userInitManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleInitManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            userInitManager.Create(new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" }, "qwerty");
            userInitManager.Create(new ApplicationUser { Email = "manager@gmail.com", UserName = "manager@gmail.com" }, "qwerty");
            userInitManager.Create(new ApplicationUser { Email = "user@gmail.com", UserName = "user@gmail.com" }, "qwerty");

            roleInitManager.Create(new IdentityRole("admin"));
            roleInitManager.Create(new IdentityRole("manager"));
            roleInitManager.Create(new IdentityRole("user"));

            userInitManager.AddToRole(userInitManager.FindByEmail("admin@gmail.com").Id, "admin");
            userInitManager.AddToRole(userInitManager.FindByEmail("manager@gmail.com").Id, "manager");
            userInitManager.AddToRole(userInitManager.FindByEmail("user@gmail.com").Id, "user");

            string testDescription =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Aliquam id arcu nec turpis rutrum ultricies nec eget dui. " +
                "Phasellus viverra eu sem at commodo. Donec ultrices, est et luctus elementum, " +
                "elit massa volutpat arcu, et dignissim risus nisl sed mauris. Duis consequat viverra nisl nec suscipit. " +
                "Pellentesque auctor nunc lectus, et vehicula libero consequat quis. ";

            db.Projects.Add(new Project { Name = "Project1", Description = testDescription, StartDate = DateTime.Now });
            db.Projects.Add(new Project { Name = "Project2", Description = testDescription, StartDate = DateTime.Now });

            db.SaveChanges();

            db.Positions.Add(new Position { Name = "creator", IdProject = 1, IdUser = userInitManager.FindByEmail("admin@gmail.com").Id });
            db.Positions.Add(new Position { Name = "creator", IdProject = 2, IdUser = userInitManager.FindByEmail("manager@gmail.com").Id });
            db.Positions.Add(new Position { Name = "performer", IdProject = 1, IdUser = userInitManager.FindByEmail("user@gmail.com").Id });
            db.Positions.Add(new Position { Name = "performer", IdProject = 2, IdUser = userInitManager.FindByEmail("user@gmail.com").Id });

            db.WorkTasks.Add(new WorkTask
            {
                Subject = "Task1 subject",
                IdPerformer = 3,
                IdProject = 1,
                Status = "in-progress",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(10),
                Description = testDescription,
            });
            db.WorkTasks.Add(new WorkTask
            {
                Subject = "Task2 subject",
                IdPerformer = 4,
                IdProject = 2,
                Status = "in-progress",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(20),
                Description = testDescription,
            });
            db.WorkTasks.Add(new WorkTask
            {
                Subject = "Task3 subject",
                IdPerformer = 4,
                IdProject = 2,
                Status = "in-progress",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(15),
                Description = testDescription,
            });

            base.Seed(db);
        }
    }
}
