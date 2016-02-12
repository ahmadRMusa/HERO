namespace HERO.Migrations.GymContext
{
    using HERO.Models.Objects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HERO.Models.GymContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\GymContext";
        }

        protected override void Seed(HERO.Models.GymContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
             
            List<Models.Objects.DayOfWeek> days = new List<Models.Objects.DayOfWeek>()
            {
                new Models.Objects.DayOfWeek { Id = (int)Day.Monday, Day = Day.Monday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Tuesday, Day = Day.Tuesday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Wednesday, Day = Day.Wednesday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Thursday, Day = Day.Thursday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Friday, Day = Day.Friday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Saturday, Day = Day.Saturday, Classes = new List<WeeklyClass>() },
                new Models.Objects.DayOfWeek { Id = (int)Day.Sunday, Day = Day.Sunday, Classes = new List<WeeklyClass>() }
            };

            foreach(var item in days)
            {
                context.DaysOfWeek.AddOrUpdate(item);
            }
        }
    }
}
