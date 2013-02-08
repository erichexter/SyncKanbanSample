using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication22.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcApplication22.Models.BoardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


        private int listId = 0;
        protected override void Seed(MvcApplication22.Models.BoardContext context)
        {
            int taskId = 0;
             
            var board = new Board() {Id=1,Name = "Program Backlog", Lists = new List<List>()};
            context.Boards.AddOrUpdate(board); 
            board.Lists.Add(CreateList("Proposed"));
            board.Lists.Add(CreateList("Approved"));
            
            DbSet<List> lists = context.Set<List>();

            lists.AddOrUpdate(board.Lists.ToArray());
            context.SaveChanges();

            var board1 = new Board() { Id = 2, Name = "Product Backlog", Lists = new List<List>() };
            context.Boards.AddOrUpdate(board1);
            context.SaveChanges();

            board1.Lists.Add(CreateList("Proposed"));
            board1.Lists.Add(CreateList("UX Flow Wireframe Design"));
            board1.Lists.Add(CreateList("Data Dictionary Design"));
            board1.Lists.Add(CreateList("Architectural Design"));
            board1.Lists.Add(CreateList("Ready For Review"));
            board1.Lists.Add(CreateList("Design Approved"));
             board1.Lists.Add(CreateList("Ready For Scheduling"));
            board1.Lists.Add(CreateList("Ready For Dev"));
            //board1.Lists.Add(CreateList("Dev Started"));
            //board1.Lists.Add(CreateList("Dev Complete"));
            //board1.Lists.Add(CreateList("DIT Test Complete"));
            //board1.Lists.Add(CreateList("SIT Ready To Test"));
            //board1.Lists.Add(CreateList("SIT Functional Test"));
            //board1.Lists.Add(CreateList("SIT Test Complete"));
            //board1.Lists.Add(CreateList("UAT/STG Sign-off"));
            //board1.Lists.Add(CreateList("Feature Complete"));
            
            lists.AddOrUpdate(board1.Lists.ToArray());
            context.SaveChanges();

            foreach (var list in board1.Lists.Take(1).Union(board.Lists.Take(1)).ToArray())
            {
                for (int i = 0; i < 30; i++)
                {
                    taskId++;
                    list.Tasks.Add(new Task() { Id = taskId, Name = "Task " + taskId });                    
                }
                context.Set<Task>().AddOrUpdate(list.Tasks.ToArray());
            }
            context.SaveChanges();

        }

        private  List CreateList(string name)
        {
            return new List() { Id = listId++, Name = name,Tasks = new List<Task>()};
        }
    }
}
