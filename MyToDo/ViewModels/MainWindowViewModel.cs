using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyToDo.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        public DelegateCommand ShowLoginCommand { get; }

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ShowLogin();

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
