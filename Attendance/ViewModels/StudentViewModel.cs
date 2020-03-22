using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Attendance.Models;

namespace Attendance.ViewModels
{
    public class StudentViewModel
    {
        public IEnumerable<Student> st_list { get; set; }
        public IEnumerable<Proxy> at_list { get; set; }
        public Student students { get; set; }
        public string extra { get; set; }
       
    }
}