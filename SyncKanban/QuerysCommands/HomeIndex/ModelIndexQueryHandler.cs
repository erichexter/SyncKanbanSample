using System.Linq;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class ModelIndexQueryHandler:IQueryHandler<HomeIndexQuery,HomeIndexModel>
    {
        public HomeIndexModel Handle(HomeIndexQuery request)
        {
            var model = new HomeIndexModel();
            using (var ctx = new BoardContext())
            {
                model.Boards = ctx.Boards.OrderBy(b => b.Name).ToList();
            }
            return model;
        }
    }
}