using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace MyToDo.WPF.Models
{
    public interface IEditable
    {
        bool IsEditing { get; set; }
    }
    public class NavigationItem : INotifyPropertyChanged, IEditable
    {
        private string _title;

        private bool _isEditing;
        public string IconPath { get; set; }
        public TodoList TodoList { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region 编辑
        private DelegateCommand startEditingCommand;
        public ICommand StartEditingCommand => startEditingCommand ??= new DelegateCommand(StartEditing);

        private DelegateCommand endEditingCommand;
        public ICommand EndEditingCommand => endEditingCommand ??= new DelegateCommand(EndEditing);

        private void EndEditing()
        {
            IsEditing = false;
        }
        private void StartEditing()
        {
            IsEditing = true;
            
        }


        #endregion
        #region 删除
        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(DeleteItem);

        private void DeleteItem()
        {
            // 通过事件或父集合来删除此项
            RequestDelete?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RequestDelete;
        #endregion
    }

    public class NavigationGroup : INotifyPropertyChanged,IEditable
    {
        private string _groupName;
        public string GroupName {
            get => _groupName;
            set {
                _groupName = value;
                OnPropertyChanged();
            }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }
        #region 编辑
        private DelegateCommand startEditingCommand;
        public ICommand StartEditingCommand => startEditingCommand ??= new DelegateCommand(StartEditing);

        private DelegateCommand endEditingCommand;
        public ICommand EndEditingCommand => endEditingCommand ??= new DelegateCommand(EndEditing);

        private void EndEditing()
        {
            IsEditing = false;
        }
        private void StartEditing()
        {
            IsEditing = true;
            // 使用 Dispatcher 确保 UI 更新完成后再操作 TextBox
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                // 通过 FocusManager 找到当前聚焦的 TextBox
                var focusedElement = Keyboard.FocusedElement as TextBox;
                if (focusedElement?.DataContext == this) // 确保是当前项的 TextBox
                {
                    focusedElement.SelectAll();
                }
            }), DispatcherPriority.Render);
        }
        #endregion
        #region 删除
        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(DeleteItem);

        private void DeleteItem()
        {
            // 通过事件或父集合来删除此项
            RequestDelete?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RequestDelete;
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<NavigationItem> Items { get; set; } = new ObservableCollection<NavigationItem>();
        public bool IsExpanded { get; set; } = true;
    }



    

}
