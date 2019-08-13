namespace TaskTrackingSystem.DAL.Repositories
{
    using System;
    using TaskTrackingSystem.DAL.DbContext;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class DataUnitOfWork : IDataUnitOfWork
    {
        private TaskTrackingSystemContext _db = new TaskTrackingSystemContext();

        private IRepository<WorkTask> _workTasks;

        private IRepository<Project> _projects;

        private IRepository<Position> _positions;

        private bool _disposed = false;

        IRepository<WorkTask> IDataUnitOfWork.WorkTasks
        {
            get
            {
                if (_workTasks == null)
                {
                    _workTasks = new WorkTaskRepository(_db);
                }

                return _workTasks;
            }
        }

        IRepository<Project> IDataUnitOfWork.Projects
        {
            get
            {
                if (_projects == null)
                {
                    _projects = new ProjectRepository(_db);
                }

                return _projects;
            }
        }

        IRepository<Position> IDataUnitOfWork.Positions
        {
            get
            {
                if (_positions == null)
                {
                    _positions = new PositionRepository(_db);
                }

                return _positions;
            }
        }

        int IDataUnitOfWork.Save()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (!disposing)
                {
                    return;
                }

                if (_db == null)
                {
                    return;
                }

                _db.Dispose();
                _db = null;
                _workTasks = null;
                _projects = null;
                _positions = null;

                _disposed = true;
            }
        }
    }
}
