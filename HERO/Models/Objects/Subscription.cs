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
        [Display(Name = "3 Month Price")]
        [DataType(DataType.Currency)]
        public decimal ThreeMonthPrice { get; set; }
        [Display(Name = "6 Month Price")]
        [DataType(DataType.Currency)]
        public decimal SixMonthPrice { get; set; }
        [Display(Name = "12 Month Price")]
        [DataType(DataType.Currency)]
        public decimal TwelveMonthPrice { get; set; }
        public virtual IList<Athlete> Athletes { get; set; }
    }
}