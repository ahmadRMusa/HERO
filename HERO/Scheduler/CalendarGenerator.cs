using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERO.Scheduler
{
    /// <summary>
    /// Creates an appointment list.
    /// </summary>
    public class CalendarGenerator : ICalendarGenerator
    {
        public IEnumerable<Class> GenerateCalendar(Period period, IEnumerable<Schedule> schedules)
        {
            List<Class> classes = new List<Class>();
            for (DateTime checkDate = period.Start; checkDate <= period.End; checkDate = checkDate.AddDays(1))
            {
                AddClassesForDate(checkDate, schedules, classes);
            }
            return classes.OrderBy(a => a.Time);
        }

        public void AddClassesForDate(DateTime checkDate, IEnumerable<Schedule> schedules, List<Class> classes)
        {
            foreach(Schedule schedule in schedules)
            {
                if (schedule.OccursOnDate(checkDate))
                {
                    classes.Add(GenerateClass(checkDate, schedule));
                }
            }
        }

        public Class GenerateClass(DateTime checkDate, Schedule schedule)
        {
            return new Class
            {
                Time = checkDate.Add(schedule.TimeOfDay)
            };
        }
    }
}
