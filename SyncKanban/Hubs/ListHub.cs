using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MvcApplication22.Models;

namespace MvcApplication22
{
    public class ListHub:Hub
    {
        public void getAllLists(int boardId)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                var lists = board.Lists.ToArray();
                foreach (var list in lists)
                {
                    list.Tasks=list.Tasks.OrderBy(t => t.Order).ToList();
                }
                Clients.Caller.allLists(lists);
            }
        }

        public void movedTask(int boardId,int taskId, int sourceListId, int destinationListId, int targetIndex)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                if (sourceListId != destinationListId)
                {
                    var source = board.Lists.First(l => l.Id == sourceListId);
                    var task = source.Tasks.First(t=>t.Id==taskId);
                    var destintation = board.Lists.First(l => l.Id == destinationListId);
                    destintation.Tasks = destintation.Tasks.OrderBy(t => t.Order).ToList();
                    destintation.InsertTask(targetIndex, task);
                    source.Tasks.Remove(task);
                    OrderTasks(source);
                    OrderTasks(destintation);
                }
                else
                {
                    var source = board.Lists.First(l => l.Id == sourceListId);
                    var task = source.Tasks.First(t=>t.Id==taskId);
                    source.Tasks.Remove(task); 
                    source.InsertTask(targetIndex, task);
                    OrderTasks(source);
                }
                ctx.SaveChanges();
            }
            Clients.Others.syncTaskMove(taskId, sourceListId, destinationListId, targetIndex);
            //Clients.OthersInGroup(boardId.ToString()).syncTaskMove(sourceIndex, sourceListId, destinationListId, targetIndex);
        }

        private static void OrderTasks(List source)
        {
            var order = 0;
            foreach (var t in source.Tasks)
            {
                t.Order = order++;
            }
        }
    }
}