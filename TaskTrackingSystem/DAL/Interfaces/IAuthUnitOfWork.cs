namespace TaskTrackingSystem.DAL.Interfaces
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.DAL.Models;

    public interface IAuthUnitOfWork : IDisposable
    {
        IUserStore<ApplicationUser> Users { get; }

        IRoleStore<IdentityRole, string> Roles { get; }

        int Save();
    }
}