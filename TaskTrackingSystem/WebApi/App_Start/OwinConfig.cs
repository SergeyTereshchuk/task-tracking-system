namespace TaskTrackingSystem
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;
    using Ninject;
    using Owin;
    using TaskTrackingSystem.BLL.Interfaces;
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
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            IKernel kernel = NinjectWebCommon.CreateKernel();

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