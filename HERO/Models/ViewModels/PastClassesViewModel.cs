using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models
{
    public class PastClassesViewModel
    {
        public int AthleteId { get; set; }
        public List<string> RecordedWods { get; set; }
        public List<Class> Classes { get; set; }
    }
}