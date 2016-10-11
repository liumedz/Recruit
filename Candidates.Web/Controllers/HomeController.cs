using Candidates.Web.Models.Home;
using System.Web.Mvc;

namespace Candidates.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }
    }
}