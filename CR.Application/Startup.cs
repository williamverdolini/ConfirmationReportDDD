using Castle.Windsor;
using CR.Application.Abstractions.Services;
using CR.Application.Infrastructure.Injection.Installers;
using CR.Application.Infrastructure.Injection.WebAPI;
using CR.Application.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;

//see: http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/
[assembly: OwinStartup(typeof(CR.Application.Startup))]

namespace CR.Application
{
    public class Startup
    {
        private readonly IWindsorContainer container;

        public Startup()
        {
            this.container = new WindsorContainer()
                            .Install(
                                    new ControllersInstaller(),
                                    new RepositoriesInstaller(),
                                    new MappersInstaller(),
                                    new ServicesInstaller(),
                                    new WorkersInstaller()
                                    );
        }

        public void Configuration(IAppBuilder app)
        {
            // Configure all AutoMapper Profiles
            AutoMapperConfig.Configure(container);

            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            
            // Configure WebApi
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(this.container));

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //Provider = new SimpleAuthorizationServerProvider()
                Provider = new CustomAuthorizationServerProvider(container.Resolve<IAuthService>())
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
