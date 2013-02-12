using System.Linq;
using System.Web.Mvc;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class BoardController : Controller
    {
        private readonly BoardContext _context;

        public BoardController(BoardContext context)
        {
            _context = context;
        }

        public BoardController() : this(new BoardContext())
        {
        }

        public ActionResult Index()
        {
            return View(_context.Boards.Select(b => new {b.Name, b.Id}).ToArray());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            return View(_context.Boards.AsNoTracking().First(board => board.Id == id));
        }

        public ActionResult Create()
        {
            return View(new Board());
        }


        [HttpPost]
        public ActionResult Create(Board model)
        {
            if (ModelState.IsValid)
            {
                _context.Boards.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            Board model = _context.Boards.First(board => board.Id == id);
            return View("create", model);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Board model)
        {
            if (ModelState.IsValid)
            {
                _context.Boards.Attach(model);
                return RedirectToAction("Index");
            }

            return View("create", model);
        }
    }
}