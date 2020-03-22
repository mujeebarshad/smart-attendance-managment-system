using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class SignUpViewModel
    {
        public User Users { get; set; }
        [Display(Name = "Re-Enter Password")]
        [Required]
        public string repassword { get; set; }
    }
}