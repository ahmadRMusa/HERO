using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class ClassReminders
    {
        public int Id { get; set; }
        public virtual Athlete Athlete { get; set; }
    }
}