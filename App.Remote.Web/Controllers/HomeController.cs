using System.Web.Mvc;

namespace Jabil.Pico.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Unauthorised()
        {
            return View();
        }
    }
}