using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HEROCrossFit.Models.Objects
{
    public class Subscription
    {
        public virtual int Id { get; set; }
        [Required]
        [Display(Name = "Subscription Name")]
        public virtual string Name { get; set; }
        [Display(Name = "Monthly Price")]
        public virtual decimal PricePerMonth { get; set; }
        [Display(Name = "6-Month Price")]
        public virtual decimal PricePerHalfYear { get; set; }
        [Display(Name = "Yearly Price")]
        public virtual decimal PricePerYear { get; set; }
        public virtual IList<Athlete> Athletes { get; set; }
    }
}