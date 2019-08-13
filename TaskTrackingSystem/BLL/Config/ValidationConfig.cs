namespace TaskTrackingSystem.BLL.Config
{
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.DAL.Models;

    internal static class ValidationConfig
    {
        public static PasswordValidator GetPasswordConfig()
        {
            var config = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
            };

            return config;
        }

        public static UserValidator<ApplicationUser> GetUserConfig( UserManager<ApplicationUser> manager)
        {
            var config = new UserValidator<ApplicationUser>(manager)
            {
                RequireUniqueEmail = true,
            };

            return config;
        }
    }
}
