using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexModel();
            using (var ctx = new BoardContext())
            {
                model.Boards = ctx.Boards.OrderBy(b => b.Name).ToList();
            }
            return View(model);
        }

        public ActionResult Board(int Id)
        {
            var model = new HomeBoardViewModel();
            model.Id = Id;
            using (var ctx = new BoardContext())
            {
                model.Board = ctx.Boards.First(board => board.Id == Id);
            }

            return View(model);
        }
    }

    public class HomeBoardViewModel
    {
        public int Id { get; set; }

        public Board Board { get; set; }
    }

    public class HomeIndexModel
    {
        public List<Board> Boards { get; set; }
    }
}