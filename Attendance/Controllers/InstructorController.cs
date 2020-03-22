using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Attendance.Models;
using Attendance.ViewModels;

namespace Attendance.Controllers
{
    public class InstructorController : Controller
    {
        private ApplicationDbContext _context;


        public InstructorController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Instructor
        public ActionResult Index()
        {
            string uname = (string)Session["UserName"];
            var info = _context.Instructors.Where(c => c.username == uname).FirstOrDefault();
            var attendance = _context.Proxies.Where(m => m.Subject == info.subject).ToList();
            List<Proxy>proxy = new List<Proxy>();
            foreach (var att in attendance)
            {
                var student = _context.Students.Where(m => m.id == att.StudentId).SingleOrDefault();
                if(student.section == info.section)
                    proxy.Add(att);
                    
            }
            //var attendance = _context.Proxies.Where(a => a.student.)
            var data = new StudentViewModel
            {
                at_list = proxy
            };
            return View(data);
        }

        public ActionResult MarkAttendance()
        {
            string uname = (string)Session["UserName"];
            var section = _context.Instructors.Where(c => c.username == uname).FirstOrDefault();
            var student = _context.Students.Where(m => m.section == section.section).ToList();
            var data = new StudentViewModel
            {
                st_list = student
            };
            return View("Sheet", data);
        }
        //public InstructorViewModel ShowAllInstructor()
        //{
        //    var instructors = _context.Instructors.ToList();
        //    var viewModel = new InstructorViewModel
        //    {
        //        Inst_list = instructors
        //    };
        //    return viewModel;
        //}

        public ActionResult InstructorLogin(Instructor instructor)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashvalue;
            var userInDb = _context.Instructors.Where(c => c.username == (string)instructor.username).FirstOrDefault();
            if (userInDb == null)
            {
                var data = new User() { userType = "Instructor does not exists!" };
                return RedirectToAction("InstructorLogin", "Home", data);
            }
            byte[] byteArray = Encoding.ASCII.GetBytes((string)instructor.password);
            hashvalue = mySHA256.ComputeHash(byteArray);
            instructor.password = Encoding.ASCII.GetString(hashvalue);
            if (instructor.password != userInDb.password)
            {
                var data = new User() { userType = "Invalid Password!" };
                return RedirectToAction("InstructorLogin", "Home", data);
            }


            Session["UserName"] = instructor.username;
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Save(StudentViewModel sm)
        {
            //Get List of Students
            string uname = (string)Session["UserName"];
            var section = _context.Instructors.Where(c => c.username == uname).FirstOrDefault();
            var student = _context.Students.Where(m => m.section == section.section).ToList();
            //Save Attendance
            string subject = sm.extra;
            List<Proxy> proxies = new List<Proxy>();

            foreach (var st in student)
            {
                Proxy proxy = new Proxy();
                var checkText = Request.Form[st.rollno];
                if (checkText == "1")
                {
                    proxy.Status = true;
                }
                else
                {
                    proxy.Status = false;
                }

                proxy.StudentId = st.id;
                proxy.Date = DateTime.Now;
                proxy.Subject = subject;
                proxies.Add(proxy);
            }
            //Add to DB
            foreach (var proxy in proxies)
            {
                _context.Proxies.Add(proxy);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}