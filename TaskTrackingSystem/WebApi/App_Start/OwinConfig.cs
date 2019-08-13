namespace TaskTrackingSystem
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using Ninject;
    using Owin;
    using System;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.BLL.Services;
    using TaskTrackingSystem.WebApi;
    using TaskTrackingSystem.WebApi.Providers;

    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var kernel = NinjectWebCommon.CreateKernel();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/account/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthProvider(kernel.Get<IUsersService>()),
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}