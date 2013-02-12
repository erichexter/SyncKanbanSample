using System.Collections.Generic;
using System.Linq;

namespace SyncKanban
{
    public static class ModelExtension
    {
        public static void InsertTask(this List list, int position, Task task)
        {
            List<Task> tasks = list.Tasks.ToList();
            tasks.Insert(position, task);
            list.Tasks = tasks.ToArray();
        }

        public static void RemoveTask(this List list, Task task)
        {
            List<Task> tasks = list.Tasks.ToList();
            tasks.Remove(task);
            list.Tasks = tasks.ToArray();
        }
    }
}