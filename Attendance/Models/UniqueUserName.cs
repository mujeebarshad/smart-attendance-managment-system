using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attendance.Models
{
    public class UniqueUserName : ValidationAttribute
    {
        private ApplicationDbContext _context;

       
        
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _context = new ApplicationDbContext();

            var user = ((User) validationContext.ObjectInstance).username;
            var userInDb = _context.Users.Where(c => c.username == user).FirstOrDefault();
            if (userInDb!=null)
            {
                return new ValidationResult("The Username already exists!");
            }

           

            return ValidationResult.Success;
           
        }

    }
}