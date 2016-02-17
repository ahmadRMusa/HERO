using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class DayOfWeekModel
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public virtual IList<WeeklyClassSetup> WeeklyClassSetups { get; set; }
    }
}