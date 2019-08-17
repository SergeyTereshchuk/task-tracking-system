namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.DTO;

    public interface IUsersService
    {
        IEnumerable<UserDTO> GetUsers();

        Task<IdentityResult> CreateAsync(string email, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(UserDTO user, string authType);

        Task<IdentityResult> DeleteAsync(string id);

        Task<UserDTO> FindAsync(string name, string password);

        Task<UserDTO> FindByIdAsync(string userId);

        Task<UserDTO> FindByEmailAsync(string userEmail);

        Task<IdentityResult> UpdateAsync(UserDTO user);

        Task<IList<string>> GetRolesAsync(string id);

        Task<IdentityResult> AddToRolesAsync(string id, string[] roles);

        Task<IdentityResult> RemoveFromRolesAsync(string id, string[] roles);
    }
}