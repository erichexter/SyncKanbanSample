using System.Linq;

namespace MvcApplication22
{
    public static class ModelExtension
    {
        public static void InsertTask(this List list, int position, Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Insert(position, task);
            list.Tasks = tasks.ToArray();
        }
        public static void RemoveTask(this List list, Task task)
        {
            var tasks = list.Tasks.ToList();
            tasks.Remove(task);
            list.Tasks = tasks.ToArray();
        }
    }
}