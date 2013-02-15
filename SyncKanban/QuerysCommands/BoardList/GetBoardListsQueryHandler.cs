using System.Linq;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Hubs
{
    public class GetBoardListsQueryHandler:IQueryHandler<GetBoardListsQuery,List[]>
    {
        public List[] Handle(GetBoardListsQuery request)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.FirstOrDefault(b => b.Id == request.Id);
                var lists = board.Lists.OrderBy(l => l.Order).ToArray();
                foreach (List list in lists)
                {
                    list.Tasks = list.Tasks.OrderBy(t => t.Order).ToList();
                }
                return lists;
            }

        }
    }
}