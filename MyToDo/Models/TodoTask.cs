using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.Models
{
    public class TodoTask
    {
        public int Id { get; set; }  // 任务唯一标识
        public string Title { get; set; }  // 任务标题
        public bool IsCompleted { get; set; }  // 是否已完成

    }
}
