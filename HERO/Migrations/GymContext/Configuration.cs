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

            if (context.DaysOfWeek.ToList().Count() == 0)
            {
                List<DayOfWeekModel> days = new List<DayOfWeekModel>()
                {
                    new DayOfWeekModel { Id = (int)DayOfWeek.Monday, Day = DayOfWeek.Monday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Tuesday, Day = DayOfWeek.Tuesday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Wednesday, Day = DayOfWeek.Wednesday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Thursday, Day = DayOfWeek.Thursday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Friday, Day = DayOfWeek.Friday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Saturday, Day = DayOfWeek.Saturday },
                    new DayOfWeekModel { Id = (int)DayOfWeek.Sunday, Day = DayOfWeek.Sunday }
                };

                foreach (var item in days)
                {
                    context.DaysOfWeek.AddOrUpdate(item);
                }
            } 
        }
    }
}
