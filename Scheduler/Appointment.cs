using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    /// <summary>
    /// The Appointment class holds information about generated appointments and does not add any functionality.
    /// </summary>
    public class Appointment
    {
        public DateTime Time { get; set; }
        public string Type { get; set; }
    }
}
