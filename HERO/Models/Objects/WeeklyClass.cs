using HERO.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class WeeklyClass
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required]
        [Display(Name = "Class Duration")]
        public float Duration { get; set; }
        [Required]
        [Display(Name = "Type of Class")]
        public string Type { get; set; }
        [Display(Name = "Max Attendance")]
        public int MaxAttendance { get; set; }
        [Required]
        [Display(Name = "Period Start")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Period End")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        public virtual IList<DayOfWeekModel> Days { get; set; }
        [Required]
        public virtual IList<Class> GeneratedClasses { get; set; }
        [NotMapped]
        public string[] SelectedDays { get; set; }
    }
}