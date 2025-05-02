using MyToDo.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        
        private readonly IDialogService _dialogService;
        public DelegateCommand ShowLoginCommand { get; }
        public ICollectionView GroupedItems { get; }

  
        public MainWindowViewModel(IDialogService dialogService)
        {
            //_dialogService = dialogService;
            // ShowLogin();
            //LoadNavigationItems();
            // 初始化时订阅删除事件
            foreach (var item in DynamicNavItems)
            {
                SubscribeToDeleteEvents(item);
            }

            DynamicNavItems.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (var newItem in e.NewItems)
                    {
                        SubscribeToDeleteEvents(newItem);
                    }
                }
            };
        }

        
        

        private void SubscribeToDeleteEvents(object item)
        {
            if (item is NavigationItem navItem)
            {
                navItem.RequestDelete += (s, e) => DynamicNavItems.Remove(navItem);
            }
            else if (item is NavigationGroup group)
            {
                group.RequestDelete += (s, e) => DynamicNavItems.Remove(group);
            }
        }
        // 添加任务命令
        private DelegateCommand _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new DelegateCommand(AddTask);
        public DelegateCommand RemoveTaskCommand { get; }

        public ObservableCollection<NavigationItem> FixedNavItems { get; } = new ObservableCollection<NavigationItem>
        {
            new NavigationItem { Title = "我的一天", IconPath = "/Resources/Images/daily.png" },
            new NavigationItem { Title = "重要", IconPath = "/Resources/Images/important.png" },
            new NavigationItem { Title = "计划", IconPath = "/Resources/Images/schedule.png" },
            new NavigationItem { Title = "任务", IconPath = "/Resources/Images/task.png" }
        };

        public ObservableCollection<object> DynamicNavItems { get; set; } = new ObservableCollection<object>
        {
                new NavigationGroup{
                    GroupName = "工作",
                    Items = new ObservableCollection<NavigationItem>
                    {
                        new NavigationItem { Title = "工作任务1", IconPath = "/Resources/Images/task.png" },
                        new NavigationItem { Title = "工作任务2", IconPath = "/Resources/Images/task.png" }
                    }
                },
                new NavigationItem { Title = "新建任务", IconPath = "/Resources/Images/add.png" },
        };

        public NavigationItem NewlyCreatedItem { get; set; }
        private void AddTask()
        {
            var newTask = new NavigationItem
            {
                Title = "新任务",
                IconPath = "/Resources/Images/task.png"
            };
            DynamicNavItems.Add(newTask);
            newTask.IsEditing = true; 
            NewlyCreatedItem = newTask; // 保存新创建的任务
        }

        #region 导航栏初始化
        private void LoadNavigationItems()
        {
            
        }
        #endregion
        private void ShowLogin()
        {
            _dialogService.ShowDialog(
                "Login",                   // 注册的 Dialog 名称
                new DialogParameters(),          // 可选：传递初始参数
                result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {

                        // 处理登录成功逻辑
                    }
                    else
                    {
                        // 登录取消或失败
                    }
                });
        }

    }
}
