using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using Attendance.Models;
using Attendance.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace Attendance.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ApplicationDbContext _context;


        public UserController()
        {
            _context = new ApplicationDbContext();
        }

       public bool ValidState(string uname)
        {
            _context = new ApplicationDbContext();

            var user = uname;
            var userInDb = _context.Instructors.Where(c => c.username == user).FirstOrDefault();
            if (userInDb != null)
            {
                return false;
            }
            //if (user == null)
            //    return new ValidationResult("The Username field is required");

            return true;

        }
        public bool ValidStudentState(string uname)
        {
            _context = new ApplicationDbContext();

            var user = uname;
            var userInDb = _context.Students.Where(c => c.rollno == user).FirstOrDefault();
            if (userInDb != null)
            {
                return false;
            }
            //if (user == null)
            //    return new ValidationResult("The Username field is required");

            return true;

        }
        public ActionResult Index(InstructorViewModel viewmodel)
        {
            if (Session["UserName"] != null)
            {
                var data = ShowAllInstructor();
                if (viewmodel.extra != "")
                    data.extra = viewmodel.extra;
                data.repassword = "";
                return View("Instructor", data);
            }
            else
            {
                var data = new User() { userType = "Invalid Login" };
                return RedirectToAction("Index", "Home", data);
            }
        }

    [HttpPost]
        public ActionResult Save(SignUpViewModel user)
        {
            user.Users.userType = "Admin";
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.All(modelState => modelState.Errors.Count == 0);
                    
                    return View("Signup");
                
            }
            var data = new User() { userType = "User Not Added!" };
            
            if (user.Users.password == user.repassword)
            {
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] hashvalue;
                if (user.Users.id == 0 || user.Users.id == null) //Adding User
                {
                     byte [] byteArray = Encoding.ASCII.GetBytes((string)user.Users.password);
                     hashvalue = mySHA256.ComputeHash(byteArray);
                     user.Users.password = Encoding.ASCII.GetString(hashvalue);
                    _context.Users.Add(user.Users);
                }
                else //Editing User
                {
                    var userInDb = _context.Users.Single(c => c.id == user.Users.id);
                    userInDb.username = user.Users.username;
                    userInDb.password = user.Users.password;
                    userInDb.userType = "Admin";
                }

                _context.SaveChanges();
                data.userType = "User has been Added!";
            }
            return RedirectToAction("Index", "Home",data);
        }
        public ActionResult Signup()
        {
            var data = _context.Users;
            var model = new SignUpViewModel()
            {
                
            };
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashvalue;
            var userInDb = _context.Users.Where(c => c.username == (string)user.username).FirstOrDefault();
            if (userInDb == null)
            {
                var data = new User() { userType = "User does not exists!" };
                return RedirectToAction("Index", "Home", data);
            }
            byte [] byteArray = Encoding.ASCII.GetBytes((string)user.password);
            hashvalue = mySHA256.ComputeHash(byteArray);
            user.password = Encoding.ASCII.GetString(hashvalue);
            if (user.password != userInDb.password)
            {
                var data = new User() { userType = "Invalid Password!" };
                return RedirectToAction("Index", "Home", data);
            }

            
            Session["UserName"] = user.username;
            var viewModel = ShowAllInstructor();
            viewModel.extra = "";
            return RedirectToAction("Index", viewModel);
        }

        [HttpPost]
        public ActionResult AddInstructor(InstructorViewModel instructor)
        {
            if (!ValidState(instructor.Instructors.username))
            {
                var data2 = ShowAllInstructor();
                data2.extra = "Username Already Exists!";
                return RedirectToAction("Index", data2);
            }
            var data1 = ShowAllInstructor();
            data1.extra = "Invalid State";
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.All(modelState => modelState.Errors.Count == 0);

                return RedirectToAction("Index",data1);

            }

            var data = ShowAllInstructor();
            data.extra = "Instructor Not Added!";
            //Encrypted Passwod
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashvalue;
            byte[] byteArray = Encoding.ASCII.GetBytes((string)instructor.Instructors.password);
            hashvalue = mySHA256.ComputeHash(byteArray);
            instructor.Instructors.password = Encoding.ASCII.GetString(hashvalue);

            byte[] hashvalue1;
            byte[] byteArray1 = Encoding.ASCII.GetBytes((string)instructor.repassword);
            hashvalue1 = mySHA256.ComputeHash(byteArray1);
            instructor.repassword = Encoding.ASCII.GetString(hashvalue1);

            if (instructor.Instructors.password == instructor.repassword)
            {
                
                if (instructor.Instructors.id == 0 || instructor.Instructors.id == null) //Adding Instructor
                {
                    //Before Image
                    HttpPostedFileBase photo = Request.Files["image"];
                    string destinationDirectory = @"C:\Users\Mujeeb Arshad\Documents\Visual Studio 2013\Projects\Attendance\Attendance\Content\assets\img\";
                    string destinationDirectory1 = destinationDirectory + photo.FileName;
                    photo.SaveAs(destinationDirectory1);
                    if (System.IO.File.Exists(destinationDirectory + instructor.Instructors.username + ".jpg"))
                    {
                        System.IO.File.Delete(destinationDirectory + instructor.Instructors.username + ".jpg");
                    }
                    System.IO.File.Move(destinationDirectory1, destinationDirectory + instructor.Instructors.username + ".jpg");
                    instructor.Instructors.image_path = "../../Content/assets/img/" + instructor.Instructors.username + ".jpg";
                    //After Image
                    
                    _context.Instructors.Add(instructor.Instructors);
                }

                _context.SaveChanges();
                data = ShowAllInstructor();
                data.extra = "Instructor has been Added!";
            }
            return RedirectToAction("Index", data);
        }

        [HttpPost]
        public ActionResult EditInstructor(InstructorEditModel instructor)
        {
            var data1 = ShowAllInstructor();
            data1.extra = "Invalid State";
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.All(modelState => modelState.Errors.Count == 0);

                return RedirectToAction("Index", data1);

            }

            var data = ShowAllInstructor();
            data.extra = "Instructor Not Edited!";
            //Encrypted Passwod
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashvalue;
            byte[] byteArray = Encoding.ASCII.GetBytes((string)instructor.password);
            hashvalue = mySHA256.ComputeHash(byteArray);
            instructor.password = Encoding.ASCII.GetString(hashvalue);

            byte[] hashvalue1;
            byte[] byteArray1 = Encoding.ASCII.GetBytes((string)instructor.repassword);
            hashvalue1 = mySHA256.ComputeHash(byteArray1);
            instructor.repassword = Encoding.ASCII.GetString(hashvalue1);

            if (instructor.password == instructor.repassword)
            {

                var instructorInDb = _context.Instructors.Single(c => c.id == instructor.id);
                instructorInDb.username = instructor.username;
                instructorInDb.name = instructor.name;
                instructorInDb.section = instructor.section;
                instructorInDb.subject = instructor.subject;
                instructorInDb.password = instructor.password;

                    //Before Image

                    HttpPostedFileBase photo = Request.Files["image"];
                    string destinationDirectory =
                        @"C:\Users\Mujeeb Arshad\Documents\Visual Studio 2013\Projects\Attendance\Attendance\Content\assets\img\";
                    string destinationDirectory1 = destinationDirectory + photo.FileName;
                    if (photo.FileName != null && photo.FileName != "")
                    {
                        photo.SaveAs(destinationDirectory1);
                        if (System.IO.File.Exists(destinationDirectory + instructor.username + ".jpg"))
                        {
                            System.IO.File.Delete(destinationDirectory + instructor.username + ".jpg");
                        }

                        System.IO.File.Move(destinationDirectory1,
                            destinationDirectory + instructor.username + ".jpg");

                        
                    }
                    instructor.image_path = "../../Content/assets/img/" + instructor.username + ".jpg";

                    
                    //After Image
                    instructorInDb.image_path = instructor.image_path;
                    
                    _context.SaveChanges();
              
               

                data = ShowAllInstructor();
                data.extra = "Instructor has been Edited!";
            }
            return RedirectToAction("Index", data);
        }

        public InstructorViewModel ShowAllInstructor()
        {
            var instructors = _context.Instructors.ToList();
            var viewModel = new InstructorViewModel
            {
                Inst_list = instructors
            };
            return viewModel;
        }
        public StudentViewModel ShowAllStudent()
        {
            var students = _context.Students.ToList();
            var viewModel = new StudentViewModel
            {
                st_list = students
            };
            return viewModel;
        }
        
        [HttpPost]
        public ActionResult Search(string username)
        {
            username = Request.Form["search"];
            var result = _context.Instructors.Where(c => c.username == username).ToList();
            var data = new InstructorViewModel
            {
                Inst_list = result
            };            

            return View("Instructor", data);

        }

        public ActionResult Edit(int id)
        {
            var instructor = _context.Instructors.SingleOrDefault(c => c.id == id);
            if (instructor == null)
                return HttpNotFound();
            Instructor ins = new Instructor();
            ins.id = instructor.id;
            ins.username = instructor.username;
            ins.password = instructor.password;
            ins.name = instructor.name;
            ins.section = instructor.section;
            ins.subject = instructor.subject;
            ins.image_path = instructor.image_path;
            var viewmodel = ShowAllInstructor();
            //var data = new InstructorViewModel
            //{
            //    Inst_list = viewmodel.Inst_list,
            //    Instructors = ins

            //};
            var data = new InstructorEditModel
            {
                id = ins.id,
                username = ins.username,
                name = ins.name,
                password = ins.password,
                section = ins.section,
                subject = ins.subject,
                image_path = ins.image_path
            };
            

            return View(data);
        }

        public ActionResult Delete(int id)
        {
            
            var instructor = _context.Instructors.SingleOrDefault(c => c.id == id);
            if (instructor == null)
                return HttpNotFound();
           
            _context.Instructors.Attach(instructor);
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();

            var data = new InstructorViewModel
            {

            };
            return RedirectToAction("Index", data);
        }


        /*********Student********/ 

        public ActionResult StudentIndex(StudentViewModel viewmodel)
        {
            var data = ShowAllStudent();
            if (viewmodel.extra != "")
                data.extra = viewmodel.extra;
            return View("Student", data);
        }

        [HttpPost]
        public ActionResult AddStudent(StudentViewModel student)
        {
            var data2 = ShowAllStudent();
            data2.extra = "Roll Number Already Exists!";
            if (!ValidStudentState(student.students.rollno))
            {
                
                return RedirectToAction("StudentIndex", data2);
            }
            var data = ShowAllStudent();
            data.extra = "Student Not Added!";

           
                if (student.students.id == 0 || student.students.id == null) //Adding Instructor
                {
                    //Before Image
                    HttpPostedFileBase photo = Request.Files["image"];
                    string destinationDirectory = @"C:\Users\Mujeeb Arshad\Documents\Visual Studio 2013\Projects\Attendance\Attendance\Content\assets\img\";
                    string destinationDirectory1 = destinationDirectory + photo.FileName;
                    photo.SaveAs(destinationDirectory1);
                    if (System.IO.File.Exists(destinationDirectory + student.students.rollno + ".jpg"))
                    {
                        System.IO.File.Delete(destinationDirectory + student.students.rollno + ".jpg");
                    }
                    System.IO.File.Move(destinationDirectory1, destinationDirectory + student.students.rollno + ".jpg");
                    student.students.img_path = "../../Content/assets/img/" + student.students.rollno + ".jpg";
                    //After Image

                    _context.Students.Add(student.students);
                }

                _context.SaveChanges();
                data = ShowAllStudent();
                data.extra = "Student has been Added!";
          
            return RedirectToAction("StudentIndex", data);
        }

        public ActionResult DeleteStudent(int id)
        {

            var student = _context.Students.SingleOrDefault(c => c.id == id);
            if (student == null)
                return HttpNotFound();

            _context.Students.Attach(student);
            _context.Students.Remove(student);
            _context.SaveChanges();

            var data = new StudentViewModel
            {

            };
            return RedirectToAction("StudentIndex", data);
        }

        public ActionResult EditStudentFunc(StudentViewModel student)
        {
            var data = ShowAllStudent();
            data.extra = "Student Not Edited!";
            
                var studentInDb = _context.Students.Single(c => c.id == student.students.id);
                studentInDb.rollno = student.students.rollno;
                studentInDb.name = student.students.name;
                studentInDb.section = student.students.section;
          
                //Before Image

                HttpPostedFileBase photo = Request.Files["image"];
                string destinationDirectory =
                    @"C:\Users\Mujeeb Arshad\Documents\Visual Studio 2013\Projects\Attendance\Attendance\Content\assets\img\";
                string destinationDirectory1 = destinationDirectory + photo.FileName;
                if (photo.FileName != null && photo.FileName != "")
                {
                    photo.SaveAs(destinationDirectory1);
                    if (System.IO.File.Exists(destinationDirectory + student.students.rollno + ".jpg"))
                    {
                        System.IO.File.Delete(destinationDirectory + student.students.rollno + ".jpg");
                    }

                    System.IO.File.Move(destinationDirectory1,
                        destinationDirectory + student.students.rollno + ".jpg");


                }
                student.students.img_path = "../../Content/assets/img/" + student.students.rollno + ".jpg";


                //After Image
                studentInDb.img_path = student.students.img_path;

                _context.SaveChanges();



                data = ShowAllStudent();
                data.extra = "Student has been Edited!";
           
            return RedirectToAction("StudentIndex", data);
        }
        public ActionResult EditStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(c => c.id == id);
            if (student == null)
                return HttpNotFound();
            Student ins = new Student();
            ins.id = student.id;
            ins.rollno = student.rollno;
            ins.name = student.name;
            ins.section = student.section;
            ins.img_path = student.img_path;
            var viewmodel = ShowAllStudent();
            //var data = new InstructorViewModel
            //{
            //    Inst_list = viewmodel.Inst_list,
            //    Instructors = ins

            //};
            var data = new StudentViewModel
            {
               students = student
            };


            return View(data);
        }
        public ActionResult SearchStudent(string rollno)
        {
            rollno = Request.Form["searchstudent"];
            var result = _context.Students.Where(c => c.rollno == rollno).ToList();
            var data = new StudentViewModel
            {
                st_list = result
            };

            return View("Student", data);

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            var data = new User() { userType = "You have been Logged out!" };
            return RedirectToAction("Index", "Home");
        }
    }
}