using MyToDo.ViewModels;
using MyToDo.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
           return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();
        }

        protected override void OnInitialized()
        {
            var loginDialog = Container.Resolve<IDialogService>();
            loginDialog.ShowDialog("LoginUC", null, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    base.OnInitialized();
                }
                else
                {
                    Environment.Exit(0);
                    
                }
            });
            
        }
    }

}
