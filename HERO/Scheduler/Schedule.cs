using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERO.Scheduler
{
    /// <summary>
    /// The Schedule class is the base class for all types that implement scheduling rules.
    /// </summary>
    public abstract class Schedule
    {
        public TimeSpan TimeOfDay { get; set; }
        public abstract bool OccursOnDate(DateTime date);
    }
}
