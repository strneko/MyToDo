using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string IconPath { get; set; }
        public ObservableCollection<TodoTask> Tasks { get; set; }= new ObservableCollection<TodoTask>();
        
    }
}
