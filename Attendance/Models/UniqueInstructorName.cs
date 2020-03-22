using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance.Models
{
    public class UniqueInstructorName : ValidationAttribute
    {
        private ApplicationDbContext _context;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _context = new ApplicationDbContext();

            var user = ((Instructor)validationContext.ObjectInstance).username;
            var userInDb = _context.Instructors.Where(c => c.username == user).FirstOrDefault();
            if (userInDb != null)
            {
                return new ValidationResult("The Username already exists!");
            }
            //if (user == null)
            //    return new ValidationResult("The Username field is required");

            return ValidationResult.Success;

        }
    }
}