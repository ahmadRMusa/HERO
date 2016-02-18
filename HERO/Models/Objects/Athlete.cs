using HERO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HERO.Models.Objects
{
    public class Athlete 
    {
        public int Id { get; set;}
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Length")]
        public int SubscriptionLength { get; set; }
        [StringLength(500)]
        public string Biography { get; set; }
        [Display(Name = "Verified")]
        public bool VerifiedUser { get; set; }
        public string ApplicationUserId { get; set; }
        [Required]
        public virtual Subscription Subscription { get; set; }
        public virtual IList<Class> Classes { get; set; }
        public virtual IList<Performance> Performances { get; set; }
        public virtual ClassReminders Reminders { get; set; }
    }
}