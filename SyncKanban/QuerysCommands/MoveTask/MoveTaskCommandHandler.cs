using System.Linq;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Hubs
{
    public class MoveTaskCommandHandler:ICommandHandler<MoveTaskCommand>
    {
        public void Handle(MoveTaskCommand message)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == message.BoardId);
                if (message.SourceListId != message.DestinationListId)
                {
                    List source = board.Lists.First(l => l.Id == message.SourceListId);
                    Task task = source.Tasks.First(t => t.Id == message.TaskId);
                    List destintation = board.Lists.First(l => l.Id == message.DestinationListId);
                    destintation.Tasks = destintation.Tasks.OrderBy(t => t.Order).ToList();
                    destintation.InsertTask(message.TargetIndex, task);
                    source.Tasks.Remove(task);
                    OrderTasks(source);
                    OrderTasks(destintation);
                }
                else
                {
                    List source = board.Lists.First(l => l.Id == message.SourceListId);
                    Task task = source.Tasks.First(t => t.Id == message.TaskId);
                    source.Tasks.Remove(task);
                    source.InsertTask(message.TargetIndex, task);
                    OrderTasks(source);
                }
                ctx.SaveChanges();
            }
            
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