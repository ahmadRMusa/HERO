using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class AthleteSignupKey
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Token { get; set; }
        [Required]
        public virtual Athlete Athlete { get; set; }
    }
}