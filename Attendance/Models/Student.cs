using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    public class Student
    {
        public int id { get; set; }
        [Display(Name="Name")]
        public string name { get; set; }
        [Display(Name = "Section")]
        public string section { get; set; }
        [Display(Name = "Roll Number")]
        public string rollno { get; set; }
        [Display(Name = "Profile Picture")]
        public string img_path { get; set; }
    }
}