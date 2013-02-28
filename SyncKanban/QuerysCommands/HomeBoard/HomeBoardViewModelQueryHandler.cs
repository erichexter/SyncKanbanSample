using System.Linq;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class HomeBoardViewModelQueryHandler:IQueryHandler<HomeBoardViewModelQuery,HomeBoardViewModel>
    {
        public HomeBoardViewModel Handle(HomeBoardViewModelQuery request)
        {
            var model = new HomeBoardViewModel();
            model.Id = request.Id;
            using (var ctx = new BoardContext())
            {
                model.Board = ctx.Boards.Include("Lists").First(board => board.Id == request.Id);
            }
            return model;
        }
    }
}