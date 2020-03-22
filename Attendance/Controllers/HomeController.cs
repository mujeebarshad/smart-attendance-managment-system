using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;



namespace Attendance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(User user)
        {
            return View(user);
        }
        public ActionResult InstructorLogin(User user)
        {
            return View(user);
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