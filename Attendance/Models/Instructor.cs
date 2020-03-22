using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    public class Instructor
    {
        public int? id { get; set; }
        [Display(Name="Name")]
        [Required]
        public string name { get; set; }
        [Display(Name = "User-Name")]
        //[UniqueInstructorName]
        public string username { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string password { get; set; }
        [Display(Name = "Section")]
        public string section { get; set; }
        public string subject { get; set; }
        public string image_path { get; set; }
    }
}