using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HERO.Models.Objects
{
    public class ClassReminders
    {
        [Key, ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        public virtual Athlete Athlete { get; set; }
        public virtual IList<WeeklyClassSetup> WeeklyClassSetups { get; set; }
        public virtual List<Class> Reminders { get; set; }
    }
}