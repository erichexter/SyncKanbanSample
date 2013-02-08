using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication22.Models;

namespace MvcApplication22.Controllers
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
            return View(Id);
        }
    }

    public class HomeIndexModel
    {
        public List<Board> Boards { get; set; }
    }
}
