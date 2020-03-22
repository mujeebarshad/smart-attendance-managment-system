using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    public class Proxy
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Student student { get; set; }
        public int StudentId { get; set; }
        public bool Status { get; set; }
        public string Subject { get; set; }
    }
}