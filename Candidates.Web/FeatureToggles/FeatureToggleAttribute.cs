using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace Candidates.Web.FeatureToggles
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class FeatureToggleAttribute : ActionFilterAttribute
    {
        private static NotesFeatureToggle _notesFeature = new NotesFeatureToggle();
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!_notesFeature.FeatureEnabled)
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