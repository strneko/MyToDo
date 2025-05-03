using MyToDo.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.WPF.ViewModels
{
    public class TodoListViewModel:BindableBase, INavigationAware
    {
        private TodoList _selectedTodoList;
        public TodoList SelectedTodoList
        {
            get => _selectedTodoList;
            set => SetProperty(ref _selectedTodoList, value);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue("todoList", out TodoList todoList))
            {
                SelectedTodoList = todoList;
            }
        }
        #region 新建任务
        private string newTaskTitle;
        public string NewTaskTitle
        {
            get => newTaskTitle; set => SetProperty(ref newTaskTitle, value);
        }


     

        private DelegateCommand _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new DelegateCommand(AddTask);
        public IEditable NewlyCreatedItem { get; set; }
        private void AddTask()
        {
            SelectedTodoList.Tasks.Add(new TodoTask
            {
                Title = NewTaskTitle,
                IsCompleted = false
            });
            NewTaskTitle = ""; // 清空输入框
        }
        #endregion
    }
}
