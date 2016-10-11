using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Linq;
using FeatureToggle.Core;

namespace Candidates.Web.FeatureToggles
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FeatureToggleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var toggles = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && typeof(IFeatureToggle).IsAssignableFrom(x) && x.Name.Contains(controllerName));
            if (toggles.Any())
            {
                var featureToggle = (IFeatureToggle) Activator.CreateInstance(toggles.First());
                if (!featureToggle.FeatureEnabled)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}