using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using APIAuth.Filters;

[assembly: OwinStartup(typeof(APIAuth.Startup))]

namespace APIAuth
{
   public class Startup
    {

       public void Configuration(IAppBuilder app)
       {
           //var config = new HttpConfiguration();
           //config.MapHttpAttributeRoutes();

           //app.UseWebApi(config);

           app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
           {
               Authority = "https://localhost:44319/identity",
               RequiredScopes = new[] { "sampleApi" }
           });

           // configure web api
           var config = new HttpConfiguration();
           config.MapHttpAttributeRoutes();

           // require authentication for all controllers
           config.Filters.Add(new AuthorizationFilterAttribute());

           app.UseWebApi(config);
       }
        //public void ConfigureOAuth(IAppBuilder app)
        //{
        //    // accept access tokens from identityserver and require a scope of 'api1'

        //    //var config = new HttpConfiguration();
        //    //config.MapHttpAttributeRoutes();

        //    //app.UseWebApi(config);
        //    app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
        //    {
        //        Authority = "https://localhost:44319",
        //        RequiredScopes = new[] { "api1" }
        //    });

        //    // configure web api
        //    var config = new HttpConfiguration();
        //    config.MapHttpAttributeRoutes();

        //    // require authentication for all controllers
        //    config.Filters.Add(new AuthorizeAttribute());

        //    app.UseWebApi(config);

        //}
    }
}
