using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTest.Models
{
    public class CalendarDate
    {
        public int CalendarDateID { get; set; }

        [DataType(DataType.Date)]

[Column(TypeName = "DateTime2")]
[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
[Display(Name = "Date")]
        public DateTime CalendarDate1 { get; set; }
    }
}