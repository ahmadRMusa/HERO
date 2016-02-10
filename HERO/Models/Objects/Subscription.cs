using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class Subscription
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subscription Name")]
        public string Name { get; set; }
        [Display(Name = "Monthly Price")]
        [DataType(DataType.Currency)]
        public decimal PricePerMonth { get; set; }
        [Display(Name = "6-Month Price")]
        [DataType(DataType.Currency)]
        public decimal PricePerHalfYear { get; set; }
        [Display(Name = "Yearly Price")]
        [DataType(DataType.Currency)]
        public decimal PricePerYear { get; set; }
        [Display(Name = "Description")]
        [StringLength(300)]
        public string Description { get; set; }
        public virtual IList<Athlete> Athletes { get; set; }
    }
}