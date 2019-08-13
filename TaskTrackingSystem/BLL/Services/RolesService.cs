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
        private readonly IAuthUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolesService(IAuthUnitOfWork roleUW, IMapper rolesMapper)
            : base(roleUW.Roles)
        {
            _unitOfWork = roleUW;
            _mapper = rolesMapper;
        }

        IEnumerable<RoleDTO> IRolesService.GetRoles()
        {
            return _mapper.Map<IEnumerable<RoleDTO>>(Roles);
        }

        async Task<IdentityResult> IRolesService.CreateAsync(string name)
        {
            var appRole = new IdentityRole { Name = name };
            var result = await base.CreateAsync(appRole);
            return result;
        }

        async Task<IdentityResult> IRolesService.DeleteAsync(RoleDTO role)
        {
            var result = await base.DeleteAsync(_mapper.Map<IdentityRole>(role));
            return result;
        }

        async Task<RoleDTO> IRolesService.FindByIdAsync(string roleId)
        {
            var result = await base.FindByIdAsync(roleId);
            return _mapper.Map<RoleDTO>(result);
        }

        async Task<RoleDTO> IRolesService.FindByNameAsync(string name)
        {
            var result = await base.FindByNameAsync(name);
            return _mapper.Map<RoleDTO>(result);
        }

        async Task<IdentityResult> IRolesService.UpdateAsync(RoleDTO role)
        {
            var result = await base.UpdateAsync(_mapper.Map<IdentityRole>(role));
            return result;
        }
    }
}
