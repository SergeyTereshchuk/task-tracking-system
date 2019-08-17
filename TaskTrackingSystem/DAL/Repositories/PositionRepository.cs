namespace TaskTrackingSystem.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TaskTrackingSystem.DAL.DbContext;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class PositionRepository : IRepository<Position>
    {
        public PositionRepository(TaskTrackingSystemContext context)
        {
            Db = context;
        }

        private TaskTrackingSystemContext Db { get; set; }

        IEnumerable<Position> IRepository<Position>.GetAll()
        {
            return Db.Positions;
        }

        Position IRepository<Position>.Get(int id)
        {
            return Db.Positions.Find(id);
        }

        Position IRepository<Position>.Create(Position item)
        {
            return Db.Positions.Add(item);
        }

        Position IRepository<Position>.Update(Position item)
        {
            Position oldItem = Db.Positions.Find(item.Id);

            Db.Entry(oldItem).CurrentValues.SetValues(item);
            Db.SaveChanges();
            return oldItem;
        }

        Position IRepository<Position>.Delete(int id)
        {
            return Db.Positions.Remove(Db.Positions.Find(id));
        }

        IEnumerable<Position> IRepository<Position>.Filter(Func<Position, bool> predicate)
        {
            return Db.Positions.Where(predicate).ToList();
        }
    }
}
