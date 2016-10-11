using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using FeatureToggle.Core;

namespace Candidates.Web.FeatureToggles
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FeatureToggleAttribute : ActionFilterAttribute
    {
        private Type _featureToggletype;

        public FeatureToggleAttribute(Type featureToggleType)
        {
            if (!typeof(IFeatureToggle).IsAssignableFrom(featureToggleType))
            {
                throw new ArgumentException("Specified class is not supported");
            }
            _featureToggletype = featureToggleType;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var featureToggle = (IFeatureToggle)Activator.CreateInstance(_featureToggletype);
            if (!featureToggle.FeatureEnabled)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}