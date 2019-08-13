namespace TaskTrackingSystem.BLL.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.Config;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class UsersService : UserManager<ApplicationUser>, IUsersService
    {
        private readonly IAuthUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IAuthUnitOfWork userUW, IMapper usersMapper)
            : base(userUW.Users)
        {
            _unitOfWork = userUW;
            _mapper = usersMapper;
            PasswordValidator = ValidationConfig.GetPasswordConfig();
            UserValidator = ValidationConfig.GetUserConfig(this);
        }

        IEnumerable<UserDTO> IUsersService.GetUsers()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(Users);
        }

        async Task<IdentityResult> IUsersService.CreateAsync(string email, string password)
        {
            var appUser = new ApplicationUser { Email = email, UserName = email };
            var result = await base.CreateAsync(appUser, password);
            return result;
        }

        async Task<ClaimsIdentity> IUsersService.CreateIdentityAsync(UserDTO user, string authType)
        {
            var appUser = await base.FindByEmailAsync(user.Email);
            var result = await base.CreateIdentityAsync(appUser, authType);
            return result;
        }

        async Task<IdentityResult> IUsersService.DeleteAsync(UserDTO user)
        {
            var result = await base.DeleteAsync(_mapper.Map<ApplicationUser>(user));
            return result;
        }

        async Task<UserDTO> IUsersService.FindAsync(string name, string password)
        {
            var result = await base.FindAsync(name, password);
            return _mapper.Map<UserDTO>(result);
        }

        async Task<UserDTO> IUsersService.FindByIdAsync(string userId)
        {
            var result = await base.FindByIdAsync(userId);
            return _mapper.Map<UserDTO>(result);
        }

        async Task<UserDTO> IUsersService.FindByEmailAsync(string userEmail)
        {
            var result = await base.FindByEmailAsync(userEmail);
            return _mapper.Map<UserDTO>(result);
        }

        async Task<IdentityResult> IUsersService.UpdateAsync(UserDTO user)
        {
            var result = await base.UpdateAsync(_mapper.Map<ApplicationUser>(user));
            return result;
        }

        async Task<IList<string>> IUsersService.GetRolesAsync(string id)
        {
            var result = await base.GetRolesAsync(id);
            return result;
        }

        async Task<IdentityResult> IUsersService.AddToRolesAsync(string id, string[] roles)
        {
            var result = await base.AddToRolesAsync(id, roles);
            return result;
        }

        async Task<IdentityResult> IUsersService.RemoveFromRolesAsync(string id, string[] roles)
        {
            var result = await base.RemoveFromRolesAsync(id, roles);
            return result;
        }
    }
}
