using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Models.ViewModels
{
    public class ManageRemindersViewModel
    {
        public List<WeeklyClassSetup> ClassesWithReminders { get; set; }
        public List<WeeklyClassSetup> ClassesNoReminders { get; set; }
    }
}