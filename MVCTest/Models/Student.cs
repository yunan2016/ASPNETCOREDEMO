using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTest.Models
{
   

    public enum EmploymentType
    {
        ToBeDetermined,
        FullTime,
        PartTime
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int? CalendarDateID { get; set; }
        public virtual CalendarDate CalendarDate { get; set; }
    }
}