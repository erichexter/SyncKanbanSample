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
                var lists = board.Lists.OrderBy(l=>l.Order).ToArray();
                foreach (var list in lists)
                {
                    list.Tasks=list.Tasks.OrderBy(t => t.Order).ToList();
                }
                
                Clients.Caller.allLists(lists);
            }
        }

        public void movedList(int boardId, int listId, int targetIndex)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                var list = board.Lists.First(l => l.Id == listId);
                
                var lists = board.Lists.ToList();
                lists.Remove(list);
                lists.Insert(targetIndex,list);
                board.Lists = lists;
                int order = 0;
                foreach (var list1 in board.Lists)
                {
                    list1.Order = order++;
                }
                ctx.SaveChanges();
            }
            Clients.Others.syncListMove(listId, targetIndex);
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