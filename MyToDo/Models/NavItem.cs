using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.WPF.Models
{
    public class NavigationItem
    {
        public string IconPath { get; set; }
        public string Title { get; set; }
    }

    public class NavigationGroup
    {
        public string GroupName { get; set; }
        public ObservableCollection<NavigationItem> Items { get; set; }
    }
}
