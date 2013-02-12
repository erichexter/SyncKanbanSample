using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SyncKanban;
using SyncKanban.Models;

namespace SyncKanban.Hubs
{
    public class ListHub : Hub
    {
        public void getAllLists(int boardId)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                List[] lists = board.Lists.OrderBy(l => l.Order).ToArray();
                foreach (List list in lists)
                {
                    list.Tasks = list.Tasks.OrderBy(t => t.Order).ToList();
                }

                Clients.Caller.allLists(lists);
            }
        }

        public void movedList(int boardId, int listId, int targetIndex)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                List list = board.Lists.First(l => l.Id == listId);

                List<List> lists = board.Lists.ToList();
                lists.Remove(list);
                lists.Insert(targetIndex, list);
                board.Lists = lists;
                int order = 0;
                foreach (List list1 in board.Lists)
                {
                    list1.Order = order++;
                }
                ctx.SaveChanges();
            }
            Clients.Others.syncListMove(listId, targetIndex);
        }

        public void movedTask(int boardId, int taskId, int sourceListId, int destinationListId, int targetIndex)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == boardId);
                if (sourceListId != destinationListId)
                {
                    List source = board.Lists.First(l => l.Id == sourceListId);
                    Task task = source.Tasks.First(t => t.Id == taskId);
                    List destintation = board.Lists.First(l => l.Id == destinationListId);
                    destintation.Tasks = destintation.Tasks.OrderBy(t => t.Order).ToList();
                    destintation.InsertTask(targetIndex, task);
                    source.Tasks.Remove(task);
                    OrderTasks(source);
                    OrderTasks(destintation);
                }
                else
                {
                    List source = board.Lists.First(l => l.Id == sourceListId);
                    Task task = source.Tasks.First(t => t.Id == taskId);
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
            int order = 0;
            foreach (Task t in source.Tasks)
            {
                t.Order = order++;
            }
        }
    }
}