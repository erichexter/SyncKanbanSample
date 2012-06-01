using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sample.Migrations;
using SignalR.Hubs;

namespace Sample
{
    public class ListHub:Hub
    {

        public void getAllLists()
        {
            using(var ctx = new DataContext())
            {
                var list = ctx.Lists.Where(d => true).Include(t=>t.Tasks).ToArray();
                Caller.allLists(list);
            }
        }

        public void movedTask(string sourceIndex, string sourceTableId,string targetTableId,string targetIndex)
        {
            using (var ctx = new DataContext())
            {
                var source = ctx.Lists.First(l => l.id == sourceTableId);
                var task = source.Tasks.ToList()[int.Parse(sourceIndex)];
                var destintation = ctx.Lists.First(l => l.id == targetTableId);
                
                destintation.InsertTask(int.Parse(targetIndex), task);
                source.Tasks.Remove(task);
                ctx.SaveChanges();
            }
            Clients.syncTaskMove(sourceIndex, sourceTableId,targetTableId,targetIndex,Context.ConnectionId);
        }
    }

    public static class ArrayExtension
    {
        public static void InsertTask(this List list,int position,Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Insert(position,task);
            list.Tasks = tasks.ToArray();
        }
        public static void RemoveTask(this List list,Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Remove(task);
            list.Tasks = tasks.ToArray();
        }
    }
}