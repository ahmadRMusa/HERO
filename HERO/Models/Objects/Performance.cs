using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class Performance
    {
        public int Id { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public virtual Athlete Athlete { get; set; }
        [Required]
        public virtual Class Class { get; set; }
        [Required]
        public virtual WOD WOD { get; set; }
    }
}