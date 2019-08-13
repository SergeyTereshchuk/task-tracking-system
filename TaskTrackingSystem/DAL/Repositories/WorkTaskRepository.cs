namespace TaskTrackingSystem.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TaskTrackingSystem.DAL.DbContext;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class WorkTaskRepository : IRepository<WorkTask>
    {
        public WorkTaskRepository(TaskTrackingSystemContext context)
        {
            Db = context;
        }

        private TaskTrackingSystemContext Db { get; set; }

        IQueryable<WorkTask> IRepository<WorkTask>.GetAll()
        {
            return Db.WorkTasks;
        }

        WorkTask IRepository<WorkTask>.Get(int id)
        {
            return Db.WorkTasks.Find(id);
        }

        WorkTask IRepository<WorkTask>.Create(WorkTask item)
        {
            return Db.WorkTasks.Add(item);
        }

        WorkTask IRepository<WorkTask>.Update(WorkTask item)
        {
            var oldItem = Db.WorkTasks.Find(item.Id);

            Db.Entry(oldItem).CurrentValues.SetValues(item);
            Db.SaveChanges();
            return oldItem;
        }

        WorkTask IRepository<WorkTask>.Delete(int id)
        {
            return Db.WorkTasks.Remove(Db.WorkTasks.Find(id));
        }

        IEnumerable<WorkTask> IRepository<WorkTask>.Filter(Func<WorkTask, bool> predicate)
        {
            return Db.WorkTasks.Where(predicate);
        }
    }
}
