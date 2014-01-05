using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace PatientControl.Web.Controllers
{
    public class HomeController : Controller 
    {
        [GET("/")]
        public ActionResult GetHome()
        {
            return View("Index");
        }
    }
}