using MyToDo.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        public DelegateCommand ShowLoginCommand { get; }
        public ICollectionView GroupedItems { get; }

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            // ShowLogin();

        }
        // 添加任务命令
        public DelegateCommand AddTaskCommand { get; }

        private void AddTask()
        {
            // NavItems.Add(new NavItem { Title = "新任务", IconPathPath = "/Resources/Images/close.png" });
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
