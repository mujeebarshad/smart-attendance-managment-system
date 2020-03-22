using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class InstructorViewModel
    {
        public IEnumerable <Instructor> Inst_list { get; set; }
        public Instructor Instructors { get; set; }
        [Display(Name = "Re-Enter Password")]
        public string repassword { get; set; }
        public string extra { get; set; }
        public HttpPostedFileBase imageFile;
    }
}