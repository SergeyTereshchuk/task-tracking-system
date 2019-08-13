namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.DTO;

    public interface IRolesService
    {
        IEnumerable<RoleDTO> GetRoles();

        Task<IdentityResult> CreateAsync(string name);

        Task<IdentityResult> DeleteAsync(RoleDTO role);

        Task<RoleDTO> FindByIdAsync(string roleId);

        Task<RoleDTO> FindByNameAsync(string name);

        Task<IdentityResult> UpdateAsync(RoleDTO role);
    }
}
