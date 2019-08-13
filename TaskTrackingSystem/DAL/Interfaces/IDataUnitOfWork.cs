namespace TaskTrackingSystem.DAL.Interfaces
{
    using System;
    using TaskTrackingSystem.DAL.Models;

    public interface IDataUnitOfWork : IDisposable
    {
        IRepository<WorkTask> WorkTasks { get; }

        IRepository<Project> Projects { get; }

        IRepository<Position> Positions { get; }

        int Save();
    }
}
