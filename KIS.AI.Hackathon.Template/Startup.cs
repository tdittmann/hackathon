namespace KIS.AI.Hackathon.Template
{
    using Owin;

    using System.Web.Http;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            app.UseWebApi(configuration);
        }
    }
}