namespace TaskTrackingSystem.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TaskTrackingSystem.DAL.DbContext;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class ProjectRepository : IRepository<Project>
    {
        public ProjectRepository(TaskTrackingSystemContext context)
        {
            Db = context;
        }

        private TaskTrackingSystemContext Db { get; set; }

        IQueryable<Project> IRepository<Project>.GetAll()
        {
            return Db.Projects;
        }

        Project IRepository<Project>.Get(int id)
        {
            return Db.Projects.Find(id);
        }

        Project IRepository<Project>.Create(Project item)
        {
            return Db.Projects.Add(item);
        }

        Project IRepository<Project>.Update(Project item)
        {
            var oldItem = Db.Projects.Find(item.Id);

            Db.Entry(oldItem).CurrentValues.SetValues(item);
            Db.SaveChanges();
            return oldItem;
        }

        Project IRepository<Project>.Delete(int id)
        {
            return Db.Projects.Remove(Db.Projects.Find(id));
        }

        IEnumerable<Project> IRepository<Project>.Filter(Func<Project, bool> predicate)
        {
            return Db.Projects.Where(predicate);
        }
    }
}
