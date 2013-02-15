using System.Collections.Generic;
using System.Linq;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Hubs
{
    public class MoveListCommandHandler:ICommandHandler<MoveListCommand>
    {
        public void Handle(MoveListCommand request)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == request.Id);
                List list = board.Lists.First(l => l.Id == request.ListId);

                List<List> lists = board.Lists.ToList();
                lists.Remove(list);
                lists.Insert(request.TargetIndex, list);
                board.Lists = lists;
                int order = 0;
                foreach (List list1 in board.Lists)
                {
                    list1.Order = order++;
                }
                ctx.SaveChanges();
            }

        }
    }
}