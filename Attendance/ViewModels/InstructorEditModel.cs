using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class InstructorEditModel
    {
        public int? id { get; set; }
        [Display(Name = "Name")]
      
        public string name { get; set; }
        [Display(Name = "User-Name")]
        
        public string username { get; set; }
        [Display(Name = "Password")]
       
        public string password { get; set; }
        [Display(Name = "Section")]
        public string section { get; set; }
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Display(Name = "Profile Picture")]
        public string image_path { get; set; }
        [Display(Name = "Re-Enter Password")]
        public string repassword { get; set; }
    }
}