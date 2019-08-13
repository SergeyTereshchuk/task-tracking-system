using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using TaskTrackingSystem.BLL.DI;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.BLL.Services;
using TaskTrackingSystem.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace TaskTrackingSystem.WebApi
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        internal static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Load(new UnitOfWorkModule());
            kernel.Load(new AutoMapperModule());
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IWorkTasksService>().To<WorkTasksService>();
            kernel.Bind<IProjectsService>().To<ProjectsService>();
            kernel.Bind<IPositionsService>().To<PositionsService>();
            kernel.Bind<IUsersService>().To<UsersService>();
            kernel.Bind<IRolesService>().To<RolesService>();
        }
    }
}