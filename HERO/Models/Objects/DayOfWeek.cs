using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class DayOfWeek
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public virtual IList<WeeklyClass> Classes { get; set; }
    }

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}