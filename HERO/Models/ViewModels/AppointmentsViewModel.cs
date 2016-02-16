using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models
{
    public class AppointmentsViewModel
    {
        public List<Class> UpcomingClasses { get; set; }
        public List<Class> PastClasses { get; set; }
    }
}