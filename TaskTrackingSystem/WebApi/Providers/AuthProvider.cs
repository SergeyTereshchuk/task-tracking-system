namespace TaskTrackingSystem.WebApi.Providers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUsersService _service;

        public AuthProvider(IUsersService usersService)
        {
            _service = usersService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserDTO user = await _service.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity identity = await _service.CreateIdentityAsync(user, context.Options.AuthenticationType);

            context.Validated(identity);
        }
    }
}