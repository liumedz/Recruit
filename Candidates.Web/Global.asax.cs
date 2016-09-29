using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Configuration;
using Candidates.DataAccess;
using System.Data.SqlClient;

namespace Candidates.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();

            var resolver = GlobalConfiguration.Configuration.DependencyResolver;
            var dbInitializer = (IDbInitializer) resolver.GetService(typeof(IDbInitializer));
            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            var csBuilder = new SqlConnectionStringBuilder(cs);
            var databaseName = csBuilder.InitialCatalog;

            csBuilder.InitialCatalog = "master";

            var created = dbInitializer.EnsureDatabaseCreated(new SqlConnection(csBuilder.ToString()), databaseName);
            
            if (created)
                dbInitializer.CreateTables(new SqlConnection(cs));
        }
    }
}