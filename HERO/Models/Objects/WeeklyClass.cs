using Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class WeeklyClass : WeeklySchedule
    {
        public int Id { get; set; }
        public float Duration { get; set; }
        public int MaxAttendance { get; set; }
        public IList<Athlete> Attendance { get; set; }
    }
}