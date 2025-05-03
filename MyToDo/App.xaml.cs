using DryIoc;
using MyToDo.ViewModels;
using MyToDo.Views;
using MyToDo.WPF.HttpClient;
using MyToDo.WPF.Views;
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
            containerRegistry.RegisterDialog<LoginWindow, LoginWindowViewModel>("Login");
            containerRegistry.GetContainer().RegisterInstance("http://localhost:5074/api/", serviceKey: "webUrl");

            containerRegistry.GetContainer().Register<HttpRestClient>(made:Parameters.Of.Type<string>(serviceKey:"webUrl"));
            containerRegistry.RegisterForNavigation<TodoListView>("TodoListView");
        }

        
    }

}
