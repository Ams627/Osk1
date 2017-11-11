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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Osk1
{

    public class KeyboardItemsControl : ItemsControl
    {
        static KeyboardItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardItemsControl), new FrameworkPropertyMetadata(typeof(KeyboardItemsControl)));
        }

        private static IEnumerable<T> GetLogicalChildren<T>(DependencyObject depObj)
        {
            var children = LogicalTreeHelper.GetChildren(depObj);
            foreach (object child in children)
            {
                if (child is T t)
                {
                    yield return t;
                }

                if (child is DependencyObject depChild)
                {
                    foreach (var childOfChild in GetLogicalChildren<T>(depChild))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var keys = GetLogicalChildren<Key1>(this);
            foreach (var key in keys)
            {
                System.Diagnostics.Debug.WriteLine($"Key {key}");
            }
        }


    }
}
