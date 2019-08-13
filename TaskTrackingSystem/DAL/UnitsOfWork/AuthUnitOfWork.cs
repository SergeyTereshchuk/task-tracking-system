namespace TaskTrackingSystem.DAL.Repositories
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.DAL.DbContext;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        private TaskTrackingSystemContext _db = new TaskTrackingSystemContext();

        private IUserStore<ApplicationUser> _users;

        private IRoleStore<IdentityRole, string> _roles;

        private bool _disposed = false;

        IUserStore<ApplicationUser> IAuthUnitOfWork.Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserStore<ApplicationUser>(_db);
                }

                return _users;
            }
        }

        IRoleStore<IdentityRole, string> IAuthUnitOfWork.Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RoleStore<IdentityRole>(_db);
                }

                return _roles;
            }
        }

        int IAuthUnitOfWork.Save()
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
                _users = null;
                _roles = null;

                _disposed = true;
            }
        }
    }
}
