using System.Collections.Generic;
using System.Linq;

namespace SyncKanban
{
    public class List
    {
        public List()
        {
        }

        public List(IList<Task> tasks)
        {
            Tasks = tasks.ToArray();
        }

        public virtual ICollection<Task> Tasks { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}