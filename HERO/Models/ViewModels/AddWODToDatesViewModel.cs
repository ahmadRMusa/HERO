using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models
{
    public class AddWODToDatesViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WODName { get; set; }
    }
}