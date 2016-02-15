using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class SingleClassSetup
    {
        [Key, ForeignKey("GeneratedClass")]
        public int ClassId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required]
        [Display(Name = "Class Duration")]
        public float Duration { get; set; }
        [Required]
        [Display(Name = "Type of Class")]
        public string Type { get; set; }
        [Display(Name = "Max Attendance")]
        public int MaxAttendance { get; set; }
        [Required]
        public virtual Class GeneratedClass { get; set; }
    }
}