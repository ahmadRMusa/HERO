using Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Time { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public float Duration { get; set; }
        [Required]
        [Display(Name = "Type of Class")]
        public string Type { get; set; }
        [Display(Name = "Max Attendance")]
        public int MaxAttendance { get; set; }
        [Required]
        public virtual IList<Athlete> Attendance { get; set; }
    }
}