namespace TaskTrackingSystem.BLL.DI
{
    using Ninject.Modules;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Repositories;

    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataUnitOfWork>().To<DataUnitOfWork>();
            Bind<IAuthUnitOfWork>().To<AuthUnitOfWork>();
        }
    }
}
