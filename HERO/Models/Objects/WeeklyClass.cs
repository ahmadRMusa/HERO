﻿using Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class WeeklyClass : WeeklySchedule
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Class Duration")]
        public float Duration { get; set; }
        [Display(Name = "Max Attendnace")]
        public int MaxAttendance { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        public virtual IList<Athlete> Attendance { get; set; }
        [Required]
        public virtual IList<DayOfWeek> Days { get; set; }
    }
}