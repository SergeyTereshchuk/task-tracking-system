namespace TaskTrackingSystem.BLL.DI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Ninject.Modules;
    using TaskTrackingSystem.BLL.Config;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.DAL.Models;

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
