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
        public bool Prescribed { get; set; }
        public virtual Athlete Athlete { get; set; }
        public virtual Class Class { get; set; }
        public virtual WOD WOD { get; set; }
        public string ScoreInput { get; set; }
        public double ScoreActual { get; set; }
        public string Description { get; set; }
    }
}