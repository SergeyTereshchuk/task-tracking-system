namespace TaskTrackingSystem.BLL.Services
{
    using System;
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
        private readonly IMapper _mapper;

        public UsersService(IAuthUnitOfWork userUW, IMapper usersMapper)
            : base(userUW.Users)
        {
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
            IdentityResult result = await base.CreateAsync(new ApplicationUser { Email = email, UserName = email }, password);
            ApplicationUser newUser = await base.FindByEmailAsync(email);
            await base.AddToRoleAsync(newUser.Id, "user");
            return result;
        }

        async Task<ClaimsIdentity> IUsersService.CreateIdentityAsync(UserDTO user, string authType)
        {
            ApplicationUser appUser = await base.FindByEmailAsync(user.Email);
            ClaimsIdentity result = await base.CreateIdentityAsync(appUser, authType);
            return result;
        }

        async Task<IdentityResult> IUsersService.DeleteAsync(string id)
        {
            ApplicationUser user = await base.FindByIdAsync(id);
            IdentityResult result = await base.DeleteAsync(user);
            return result;
        }

        async Task<UserDTO> IUsersService.FindAsync(string name, string password)
        {
            return _mapper.Map<UserDTO>(await base.FindAsync(name, password));
        }

        async Task<UserDTO> IUsersService.FindByIdAsync(string userId)
        {
            return _mapper.Map<UserDTO>(await base.FindByIdAsync(userId));
        }

        async Task<UserDTO> IUsersService.FindByEmailAsync(string userEmail)
        {
            return _mapper.Map<UserDTO>(await base.FindByEmailAsync(userEmail));
        }

        async Task<IdentityResult> IUsersService.UpdateAsync(UserDTO user)
        {
            return await base.UpdateAsync(_mapper.Map<ApplicationUser>(user));
        }

        async Task<IList<string>> IUsersService.GetRolesAsync(string id)
        {
            return await base.GetRolesAsync(id);
        }

        async Task<IdentityResult> IUsersService.AddToRolesAsync(string id, string[] roles)
        {
            return await base.AddToRolesAsync(id, roles);
        }

        async Task<IdentityResult> IUsersService.RemoveFromRolesAsync(string id, string[] roles)
        {
            return await base.RemoveFromRolesAsync(id, roles);
        }
    }
}
