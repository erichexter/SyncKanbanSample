using System.Collections.Generic;

namespace Sample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        private static Task[] CreateTasks(int count)
        {
            var students = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                students.Add(NextStudent(i));
            }

            return students.ToArray();
        }

        private static Task NextStudent(int i)
        {
            return new Task() { Name = "task " +i ,Order = i};
        }

 

        protected override void Seed(DataContext context)
        {
            
            context.Lists.AddOrUpdate(new List(CreateTasks(3))
            {
                id = "Ready",

            });
            context.Lists.AddOrUpdate(new List(CreateTasks(4))
            {
                id = "Coding",

            });
            context.Lists.AddOrUpdate(new List(CreateTasks(3))
            {
                id = "Review",

            });
            context.Lists.AddOrUpdate(new List(CreateTasks(8))
            {
                id = "Done",
            });
        }
    }

    internal class DataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext,Configuration>());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<List> Lists { get; set; } 
    }
}
