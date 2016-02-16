using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models
{
    public class AddWodViewModel
    {
        public WOD WOD { get; set; }
        public int? ClassId { get; set; }
    }
}