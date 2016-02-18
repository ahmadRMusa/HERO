using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class Reminder
    {
        public string Message { get; set; } 
        public ReminderType Type { get; set; }
        public int ClickId { get; set; }
        public DateTime? Date { get; set; }
    }

    public enum ReminderType
    {
        [Display(Name = "Upcoming Class")]
        UpcomingClass=1,
        [Display(Name = "Unrecorded Performance")]
        RecordWod=2
    }
}