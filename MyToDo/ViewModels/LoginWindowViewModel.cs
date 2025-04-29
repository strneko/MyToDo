using MyToDo.WPF.DTOs;
using MyToDo.WPF.HttpClient;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyToDo.ViewModels
{
    internal class LoginWindowViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "ToDo";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand LoginCommand { get; }

        private readonly HttpRestClient _httpRestClient;

        #region 账户绑定
        private AccountInfoDTO _accountInfoDTO;
        public AccountInfoDTO AccountInfoDTO
        {
            get { return _accountInfoDTO; }
            set
            {
                _accountInfoDTO = value;
                RaisePropertyChanged();
            }
        }
        #endregion
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

        public LoginWindowViewModel(HttpRestClient httpRestClient)
        {
            LoginCommand = new DelegateCommand(Login);

            _httpRestClient=httpRestClient;

            AccountInfoDTO = new AccountInfoDTO();
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(AccountInfoDTO.AccountEmail) || string.IsNullOrEmpty(AccountInfoDTO.AccountPassword))
            {
                MessageBox.Show("请输入邮箱和密码");
                return;
            }

            APIRequest aPIRequest = new APIRequest();
            aPIRequest.Route = "/Login";
            aPIRequest.Method = RestSharp.Method.Post;
            aPIRequest.Parameters = AccountInfoDTO;
            APIResponse response = _httpRestClient.Execute(aPIRequest);

            if(response.StatusCode == 200)
            {
                MessageBox.Show("登录成功");
                IDialogResult dialogResult = new DialogResult(ButtonResult.OK);
                RequestClose?.Invoke(dialogResult);
            }
            else
            {
                MessageBox.Show(response.Message);
            }
        }
    }
}
