using System.Linq;
using System.Web.Http;
using SyncKanban.Models;

namespace SyncKanban.Controllers.Api
{
    public class BoardTaskController : ApiController
    {
        // POST api/boardtask
        public Task Post([FromBody] CreateTask value)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.First(b => b.Id == value.BoardId);
                var t = new Task {Name = value.name};
                board.Lists.First().InsertTask(0, t);
                ctx.Set<Task>().Add(t);
                ctx.SaveChanges();
                return t;
            }
        }
    }

    public class CreateTask
    {
        public int BoardId;
        public string name;
    }
}