using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha_irina_i_karina
{
    public class TaskItem
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(string name)
        {
            Name = name;
            IsCompleted = false;
        }
    }

}
