using System.Data.Entity;

namespace SyncKanban.Models
{
    public class BoardContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
    }
}