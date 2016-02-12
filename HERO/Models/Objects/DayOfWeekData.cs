using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class DayOfWeekData
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public virtual IList<WeeklyClass> Classes { get; set; }
    }

    public enum Day
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }
}