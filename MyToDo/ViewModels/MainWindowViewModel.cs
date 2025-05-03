using ImTools;
using MyToDo.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
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
        private readonly IRegionManager _regionManager;
        public DelegateCommand ShowLoginCommand { get; }
        public DelegateCommand<NavigationItem> NavigateCommand { get; }


        public ObservableCollection<NavigationItem> FixedNavItems { get; } = new ObservableCollection<NavigationItem>
        {
            
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




        public MainWindowViewModel(IRegionManager regionManager)
        {
            //_dialogService = dialogService;
            // ShowLogin();
            LoadNavigationItems();

           
            _regionManager =regionManager;
            // 订阅导航项点击事件
            NavigateCommand = new DelegateCommand<NavigationItem>(NavigateToTodoList);

            // 初始化时订阅删除事件
            foreach (var item in DynamicNavItems)
            {
                SubscribeToDeleteEvents(item);
                if(item is NavigationGroup group)
                {
                    
                    foreach (var subItem in group.Items)
                    {
                        SubscribeToDeleteEvents(subItem,group);
                    }
                }
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

        
        
        private void SubscribeToDeleteEvents(object item, NavigationGroup parentGroup=null)
        {
            if (item is NavigationItem navItem)
            {
                
                if(parentGroup==null)
                    navItem.RequestDelete += (s, e) => DynamicNavItems.Remove(navItem);
                else navItem.RequestDelete += (s, e) => parentGroup.Items.Remove(navItem);
            }
            else if (item is NavigationGroup group)
            {
                group.RequestDelete += (s, e) => DynamicNavItems.Remove(group);
            }
        }



        #region 新建列表

        private DelegateCommand _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new DelegateCommand(AddTask);
        public IEditable NewlyCreatedItem { get; set; }
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
        #endregion

        #region 新建列表组
        private DelegateCommand _addGroupCommand;
        public ICommand AddGroupCommand => _addGroupCommand ??= new DelegateCommand(AddGroup);
        private void AddGroup()
        {
            var newGroup = new NavigationGroup
            {
                GroupName = "新建组",
                Items = new ObservableCollection<NavigationItem>()
            };
            DynamicNavItems.Add(newGroup);
            newGroup.IsEditing = true;
            NewlyCreatedItem = newGroup; // 保存新创建的任务
        }
        
        #endregion
 


        #region 导航栏初始化
        private void LoadNavigationItems()
        {

            FixedNavItems.Add(new NavigationItem
            {
                Title = "我的一天",
                IconPath = "/Resources/Images/task.png",
                TodoList = new TodoList
                {
                    Id = 1,
                    Title = "我的一天",
                    Tasks = new ObservableCollection<TodoTask>
                    {
                        new TodoTask { Id = 1, Title = "完成项目提案", IsCompleted = false },
                        new TodoTask { Id = 2, Title = "回复客户邮件", IsCompleted = true }
                    }
                }
            });
            FixedNavItems.Add(new NavigationItem
            {
                Title = "重要",
                IconPath = "/Resources/Images/important.png",
                TodoList = new TodoList
                {
                    Id = 2,
                    Title = "重要",
                    Tasks = new ObservableCollection<TodoTask>
                    {
                        
                    }
                }
            });
            FixedNavItems.Add(new NavigationItem
            {
                Title = "计划",
                IconPath = "/Resources/Images/schedule.png",
                TodoList = new TodoList
                {
                    Id = 3,
                    Title = "计划",
                    Tasks = new ObservableCollection<TodoTask>
                    {

                    }
                }
            });
            FixedNavItems.Add(new NavigationItem
            {
                Title = "任务",
                IconPath = "/Resources/Images/task.png",    
                TodoList = new TodoList
                {
                    Id = 4,
                    Title = "任务",
                    Tasks = new ObservableCollection<TodoTask>
                    {

                    }
                }
            });
        }
        #endregion

        private void NavigateToTodoList(NavigationItem item)
        {
            if (item == null) return;

            var parameters = new NavigationParameters();
            parameters.Add("todoList", item.TodoList); 

            _regionManager.RequestNavigate("ContentRegion", "TodoListView", parameters);
        }
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
