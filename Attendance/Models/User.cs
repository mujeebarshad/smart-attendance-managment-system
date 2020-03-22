using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    public class User
    {
        public int? id { get; set; }
        [Display(Name="User Name")]
        [UniqueUserName]
        public string username { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string password { get; set; }
       
        public string userType { get; set; }
    }
}