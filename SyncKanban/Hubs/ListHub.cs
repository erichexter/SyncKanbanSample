using Microsoft.AspNet.SignalR;
using ShortBus;
using SyncKanban;

namespace SyncKanban.Hubs
{
    public class ListHub : Hub
    {
        private IMediator _mediator = StructureMap.ObjectFactory.GetInstance<IMediator>();
        
        public void getAllLists(int boardId)
        {
            var response = _mediator.Request(new GetBoardListsQuery() {Id = boardId});
            Clients.Caller.allLists(response.Data);
        }

        public void movedList(int boardId, int listId, int targetIndex)
        {
            var response =_mediator.Send(new MoveListCommand() {Id = boardId, ListId = listId, TargetIndex = targetIndex});
            if (!response.HasException())
            {
                Clients.Others.syncListMove(listId, targetIndex);
            }
        }

        public void movedTask(int boardId, int taskId, int sourceListId, int destinationListId, int targetIndex)
        {
            var response = _mediator.Send(new MoveTaskCommand(){BoardId=boardId,DestinationListId=destinationListId,SourceListId=sourceListId,TargetIndex = targetIndex,TaskId = taskId});
            if (!response.HasException())
            {
                Clients.Others.syncTaskMove(taskId, sourceListId, destinationListId, targetIndex);
            }
        }
    }
}