namespace TaskTrackingSystem.BLL.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.DAL.Interfaces;

    public class RolesService : RoleManager<IdentityRole>, IRolesService
    {
        private readonly IMapper _mapper;

        public RolesService(IAuthUnitOfWork roleUW, IMapper rolesMapper)
            : base(roleUW.Roles)
        {
            _mapper = rolesMapper;
        }

        IEnumerable<RoleDTO> IRolesService.GetRoles()
        {
            return _mapper.Map<IEnumerable<RoleDTO>>(Roles);
        }

        async Task<IdentityResult> IRolesService.CreateAsync(string name)
        {
            return await base.CreateAsync(new IdentityRole { Name = name });
        }

        async Task<IdentityResult> IRolesService.DeleteAsync(RoleDTO role)
        {
            return await base.DeleteAsync(_mapper.Map<IdentityRole>(role)); ;
        }

        async Task<RoleDTO> IRolesService.FindByIdAsync(string roleId)
        {
            return _mapper.Map<RoleDTO>(await base.FindByIdAsync(roleId));
        }

        async Task<RoleDTO> IRolesService.FindByNameAsync(string name)
        {
            return _mapper.Map<RoleDTO>(await base.FindByNameAsync(name));
        }

        async Task<IdentityResult> IRolesService.UpdateAsync(RoleDTO role)
        {
            return await base.UpdateAsync(_mapper.Map<IdentityRole>(role));
        }
    }
}
