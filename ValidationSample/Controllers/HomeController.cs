using System;
using System.Web.Mvc;
using ValidationSample.Models;

namespace ValidationSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View(new ScheduleModel() {ScheduleEnd = DateTime.UtcNow.ToString("o") });
        }

        [HttpPost]
        public ActionResult Index(ScheduleModel model)
        {

            if (ModelState.IsValid)
            {
                // Do Some Stuff...
            }
            else
            {
                // Log Issues....
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}