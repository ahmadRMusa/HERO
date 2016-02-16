using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class WOD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WODScoring Scoring { get; set; }
        public string Description { get; set; }
        public IList<Class> Classes { get; set; }
    }

    public enum WODScoring
    {
        [Display(Name = "Total Reps")]
        TotalReps = 1,
        [Display(Name = "Total Rounds")]
        TotalRounds = 2,
        [Display(Name = "Total Time")]
        TotalTime = 3
    }
}

