using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SyncKanban.Models;

namespace SyncKanban.Controllers
{
    public class BoardListApiController : ApiController
    {
        // GET api/boardapi
        public IEnumerable<string> Get()
        {
            return new[] {"this is from the boardlistapi controller", "value2"};
        }

        // GET api/boardapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/boardapi
        public List Post([FromBody] CreateList input)
        {
            using (var ctx = new BoardContext())
            {
                Board board = ctx.Boards.Where(b => b.Id == input.BoardId).First();
                var list = new List {Name = input.name};
                board.Lists.Add(list);
                ctx.Set<List>().Add(list);
                ctx.SaveChanges();
                return list;
            }
        }

        // PUT api/boardapi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/boardapi/5
        public void Delete(int id)
        {
        }
    }

    public class CreateList
    {
        public int BoardId;
        public string name;
    }
}