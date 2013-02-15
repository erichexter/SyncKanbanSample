using ShortBus;

namespace SyncKanban.Hubs
{
    public class MoveListCommand:ICommand
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public int TargetIndex { get; set; }
    }
}