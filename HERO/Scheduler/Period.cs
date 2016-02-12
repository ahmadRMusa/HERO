using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERO.Scheduler
{
    /// <summary>
    /// The Period class holds a start and end date that are set using the constructor and held in read-only DateTime properties.
    /// </summary>
    public class Period
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public Period(DateTime start, DateTime end)
        {
            Start = start.Date;
            End = end.Date;

            if (Start > End) throw new ArgumentException("The start date may not be after the end date.");
        }
    }
}
