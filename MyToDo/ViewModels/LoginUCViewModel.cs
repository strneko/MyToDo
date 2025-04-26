using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    internal class LoginUCViewModel : IDialogAware
    {
        public string Title { get; set; }= "To Do Login";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand LoginCommand { get; set; }
        LoginUCViewModel()
        {
            LoginCommand = new DelegateCommand(OnLogin);
        }
        
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
            //throw new NotImplementedException();
        }

        private void OnLogin()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
        }
    }
}
