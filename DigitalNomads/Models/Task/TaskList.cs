using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNomads.Models.Task
{
    public class TaskList
    {
        public IList<TaskRes> FinishedTasks { get; set; }
        public IList<TaskRes> UnfinishedTasks { get; set; }
    }
}
