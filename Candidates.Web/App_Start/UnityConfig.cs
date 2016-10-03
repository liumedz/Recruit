using Candidates.DataAccess;
using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository;
using Candidates.DataAccess.Repository.Abstractions;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Unity.WebApi;

namespace Candidates.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            container.RegisterType<IBaseRepository<Candidate>, BaseRepository<Candidate>>(new InjectionConstructor(new SqlConnection(cs)));
            container.RegisterType<IDbInitializer, DbInitializer>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}