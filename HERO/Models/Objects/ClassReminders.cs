using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class ClassReminders
    {
        [Key, ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        public virtual Athlete Athlete { get; set; }
        public IList<Class> Reminders { get; set; }
    }
}