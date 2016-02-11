using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    /// <summary>
    /// Creates an appointment list.
    /// </summary>
    public class CalendarGenerator
    {
        public IEnumerable<Appointment> GenerateCalendar(Period period, IEnumerable<Schedule> schedules)
        {
            List<Appointment> appointments = new List<Appointment>();
            for (DateTime checkDate = period.Start; checkDate <= period.End; checkDate = checkDate.AddDays(1))
            {
                AddAppointmentsForDate(checkDate, schedules, appointments);
            }
            return appointments.OrderBy(a => a.Time);
        }

        public void AddAppointmentsForDate(DateTime checkDate, IEnumerable<Schedule> schedules, List<Appointment> appointments)
        {
            foreach(Schedule schedule in schedules)
            {
                if (schedule.OccursOnDate(checkDate))
                {
                    appointments.Add(GenerateAppointment(checkDate, schedule));
                }
            }
        }

        public Appointment GenerateAppointment(DateTime checkDate, Schedule schedule)
        {
            return new Appointment
            {
                Name = schedule.Name,
                Time = checkDate.Add(schedule.TimeOfDay)
            };
        }
    }
}
