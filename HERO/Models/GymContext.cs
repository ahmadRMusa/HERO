namespace HERO.Models
{
    using HERO.Models.Objects;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GymContext : DbContext
    {
        // Your context has been configured to use a 'GymContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HERO.Models.GymContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'GymContext' 
        // connection string in the application configuration file.
        public GymContext()
            : base("name=GymContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<AthleteSignupKey> AthleteSignupKeys { get; set; }
        public virtual DbSet<WeeklyClassSetup> WeeklyClasses { get; set; }
        public virtual DbSet<SingleClassSetup> SingleClasses { get; set; }
        public virtual DbSet<DayOfWeekModel> DaysOfWeek { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<WOD> WODs { get; set; }

        public System.Data.Entity.DbSet<HERO.Models.Objects.Performance> Performances { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}