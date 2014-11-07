using System.Web.Http;
using JayLabs.AuthorizationServer.Sample;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Diagnostics;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace JayLabs.AuthorizationServer.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            StartupAuth.Configuration(app);
            app.UseStageMarker(PipelineStage.Authenticate); // Needed for IIS pipeline
            app.UseWebApi(config);
            //app.UseWelcomePage();
        }
    }
}
