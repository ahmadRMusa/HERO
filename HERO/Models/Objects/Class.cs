using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HERO.Models.Objects
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int MaxAttendance { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public virtual IList<Athlete> Attendance { get; set; }
    }
}