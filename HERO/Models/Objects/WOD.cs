using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class WOD
    {
        public int Id { get; set; }
        public WODScoring Scoring { get; set; }
        public string Description { get; set; }
        public IList<Class> Classes { get; set; }
    }

    public enum WODScoring
    {
        TotalReps = 1,
        TotalRounds = 2,
        TotalTime = 3
    }
}

