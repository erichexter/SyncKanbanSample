using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication22.Models
{
    public class BoardContext:DbContext
    {
        public DbSet<Board> Boards { get; set; }
    }
}