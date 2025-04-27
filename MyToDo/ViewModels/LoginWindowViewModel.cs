using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    internal class LoginWindowViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; }="ToDo";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand LoginCommand { get; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            //throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public LoginWindowViewModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                // 验证逻辑...
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            });
        }
    }
}
