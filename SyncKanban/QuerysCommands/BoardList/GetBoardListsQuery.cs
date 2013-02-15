using ShortBus;

namespace SyncKanban.Hubs
{
    public class GetBoardListsQuery:IQuery<List[]>
    {
        public int Id { get; set; }
    }
}