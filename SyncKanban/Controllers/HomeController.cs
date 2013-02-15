using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using ShortBus;

namespace SyncKanban.Controllers
{
    public class HomeController : BootstrapBaseController
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var response = _mediator.Request(new HomeIndexQuery());
            return View(response.Data);
        }

        public ActionResult Board(int Id)
        {
            var response = _mediator.Request(new HomeBoardViewModelQuery(){Id = Id});
            if(response.HasException())
                Error(response.Exception.Message);
            return View(response.Data);
        }
    }
}