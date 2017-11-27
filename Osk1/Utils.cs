using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Osk1
{
    static class Utils
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                var n = VisualTreeHelper.GetChildrenCount(depObj);
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

        public static void PrintChildren(string prefix, DependencyObject dop)
        {
            var children = FindVisualChildren<DependencyObject>(dop);
            foreach (var child in children)
            {
                System.Diagnostics.Debug.WriteLine($"{prefix}: {child.GetType()}");
            }
        }

    }
}
