using System.Collections.Generic;
using System.Linq;

namespace Sample
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

        public virtual ICollection<Task> Tasks { get;  set; }
        public string id { get; set; }
    }
}