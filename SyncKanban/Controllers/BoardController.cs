using System.Linq;
using System.Web.Mvc;
using ShortBus;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class BoardController : Controller
    {
        private readonly BoardContext _context;
        private readonly IMediator _mediator;

        public BoardController(BoardContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        public ActionResult Index()
        {
            var response = _mediator.Request(new BoardListQuery());                
            return View(response.Data);
        }


        public ActionResult Details(int id)
        {
            var response = _mediator.Request(new BoardQuery(){Id = id});
            return View(response.Data);
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

    public class BoardQuery:IQuery<Board>
    {
        public int Id { get; set; }
    }

    public class BoardQueryHandler:IQueryHandler<BoardQuery,Board>
    {
        public Board Handle(BoardQuery request)
        {
            using (var _context = new BoardContext())
            {
                return _context.Boards.AsNoTracking().Include("Lists.Name").First(board => board.Id == request.Id);
            }
        }
    }
    public class BoardListQuery:IQuery<BoardList[]>
    {
    }
    public class BoardListQueryHandler:IQueryHandler<BoardListQuery , BoardList[]>
    {

        public BoardList[] Handle(BoardListQuery request)
        {
            using (var _context = new BoardContext())
            {
                return _context.Boards.Select(b => new BoardList() { Name = b.Name, Id = b.Id }).ToArray();
            }
        }
    }

    public class BoardList
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
       
    
}