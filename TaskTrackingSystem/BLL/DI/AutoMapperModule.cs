namespace TaskTrackingSystem.BLL.DI
{
    using AutoMapper;
    using Ninject.Modules;
    using TaskTrackingSystem.BLL.Config;

    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(GetMapper).InSingletonScope();
        }

        private IMapper GetMapper(Ninject.Activation.IContext context)
        {
            IMapper mapper = AutoMapperConfig.GetConfig(context).CreateMapper();

            return mapper;
        }
    }
}
