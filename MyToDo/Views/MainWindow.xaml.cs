using MyToDo.ViewModels;
using MyToDo.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyToDo.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox textBox && textBox.DataContext is NavigationItem item)
            {
                // 按下回车键时退出编辑模式
                item.IsEditing = false;

                // 将焦点移出文本框
                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                e.Handled = true;
            }
            else if (e.Key == Key.Escape && sender is TextBox textBoxEsc && textBoxEsc.DataContext is NavigationItem itemEsc)
            {
                
                itemEsc.IsEditing = false;
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is NavigationItem item)
            {
                // 失去焦点时退出编辑模式
                item.IsEditing = false;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is NavigationItem item)
            {
                // 获取焦点时进入编辑模式
                item.IsEditing = true;
                textBox.SelectAll();
            }
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox &&
                textBox.DataContext is IEditable item &&
                DataContext is MainWindowViewModel vm &&
                item == vm.NewlyCreatedItem)
            {
                // 新创建的项，自动获取焦点
                textBox.Focus();
                textBox.SelectAll();

                // 清除标记，避免下次加载时再次触发
                vm.NewlyCreatedItem = null;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 检查点击的是否是TextBox或它的父元素
            if (e.OriginalSource is DependencyObject source)
            {
                var textBox = FindVisualParent<TextBox>(source);
                if (textBox == null) // 如果点击的不是TextBox
                {
                    // 结束所有正在编辑的项目
                    EndAllEditing();
                }
            }
        }
        private void EndAllEditing()
        {
            if (DataContext is MainWindowViewModel vm)
            {
                // 结束动态列表项的编辑
                foreach (var item in vm.DynamicNavItems)
                {
                    if (item is IEditable editableItem)
                    {
                        editableItem.IsEditing = false;
                    }

                    // 如果是组，递归处理子项
                    if (item is NavigationGroup group)
                    {
                        foreach (var subItem in group.Items.OfType<IEditable>())
                        {
                            subItem.IsEditing = false;
                        }
                    }
                }
            }
        }

        // 辅助方法：查找视觉树中的父元素
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            return parent is T ? parent as T : FindVisualParent<T>(parent);
        }

        private void ItemsControl_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is false) // 如果焦点离开 ItemsControl
            {
                EndAllEditing();
            }
        }
    }
}
