using ShortBus;

namespace SyncKanban.Hubs
{
    public class MoveTaskCommand:ICommand
    {
        public int BoardId { get; set; }

        public int DestinationListId { get; set; }

        public int SourceListId { get; set; }

        public int TaskId { get; set; }

        public int TargetIndex { get; set; }
    }
}