using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Servicos.App_Start;
using Servicos.Bundles.Core.Authentication;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
// Content-Type application/x-www-form-urlencoded
// grant_type=password&username=&password=
namespace Servicos
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.RegisterServices());

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            int duracaoToken = int.Parse(ConfigurationManager.AppSettings["DURACAO_TOKEN"]);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/usuarios/autenticar"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(duracaoToken),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}