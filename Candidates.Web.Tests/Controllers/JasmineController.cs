using System;
using System.Web.Mvc;

namespace Candidates.Web.Tests.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
