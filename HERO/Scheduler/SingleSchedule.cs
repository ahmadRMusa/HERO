using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERO.Scheduler
{
    /// <summary>
    /// The simplest scheduling class allows for a single appointment at a given date and time. 
    /// </summary>
    public class SingleSchedule : Schedule
    {
        public DateTime Date { get; set; }
        public override bool OccursOnDate(DateTime date)
        {
            return Date.Date == date;
        }
    }
}
