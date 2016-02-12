using HERO.Models.Objects;
using HERO.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERO.Scheduler
{
    public interface ICalendarGenerator
    {
        IEnumerable<Class> GenerateCalendar(Period period, IEnumerable<Schedule> schedules);
        void AddClassesForDate(DateTime checkDate, IEnumerable<Schedule> schedules, List<Class> classes);
        Class GenerateClass(DateTime checkDate, Schedule schedule);
    }
}
