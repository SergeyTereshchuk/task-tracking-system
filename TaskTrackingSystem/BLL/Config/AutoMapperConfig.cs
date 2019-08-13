namespace TaskTrackingSystem.BLL.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.DAL.Models;

    internal static class AutoMapperConfig
    {
        public static MapperConfiguration GetConfig(Ninject.Activation.IContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(type => context.Kernel.GetType());
                cfg.CreateMap<WorkTask, WorkTaskDTO>().ReverseMap().MaxDepth(1);
                cfg.CreateMap<Project, ProjectDTO>().ReverseMap().MaxDepth(1);
                cfg.CreateMap<Position, PositionDTO>().ReverseMap().MaxDepth(1);

                cfg.CreateMap<ApplicationUser, UserDTO>().ReverseMap().MaxDepth(1);
                cfg.CreateMap<IdentityRole, RoleDTO>().ReverseMap().MaxDepth(1);
            });

            return config;
        }
    }
}
