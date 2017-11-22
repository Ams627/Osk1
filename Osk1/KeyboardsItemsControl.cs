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

        public KeyboardItemsControl()
        {
            this.Loaded += KeyboardItemsControl_Loaded;
            this.LayoutUpdated += (s, e) => { System.Diagnostics.Debug.WriteLine("updated"); };
            ItemContainerGenerator.StatusChanged += (s, e) =>
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    UIElement uiElement =
                        (UIElement)ItemContainerGenerator.ContainerFromIndex(i);

                    if (uiElement is ContentPresenter cp)
                    {
                        cp.ApplyTemplate();
                        System.Diagnostics.Debug.WriteLine("");
                    }
                    System.Diagnostics.Debug.WriteLine("");
                }
            };
        }

        private void KeyboardItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    UIElement uiElement =
                        (UIElement)ItemContainerGenerator.ContainerFromIndex(i);

                    if (uiElement is ContentControl cc)
                    {
                        cc.ApplyTemplate();
                        var t = cc.ContentTemplate;

                        System.Diagnostics.Debug.WriteLine("");
                    }

                    if (uiElement is ContentPresenter cp && cp.Content is Key1)
                    {
                        cp.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            var result = cp.ApplyTemplate();
                            var template = cp.ContentTemplate;

                            System.Diagnostics.Debug.WriteLine("");

                        }));
                    }
                }
            }));
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
                else if (child is DependencyObject depChild)
                {
                    foreach (var childOfChild in GetLogicalChildren<T>(depChild))
                    {
                        if (childOfChild is T t2)
                        {
                            yield return t2;
                        }
                    }
                }
            }
        }

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
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var allVisualElements = FindVisualChildren<DependencyObject>(this);
            foreach (var element in allVisualElements)
            {
                if (element is FrameworkElement fe)
                {
                    if (fe.Tag != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Type {fe.GetType()}, tag: {fe.Tag}");
                    }
                }
            }

            var borders = FindVisualChildren<Border>(this);
            foreach (var border in borders)
            {
                border.MouseLeftButtonDown += (s, e)=> {
                    var type = s.GetType();
                    System.Diagnostics.Debug.WriteLine($"s is {type}");
                    if (e.OriginalSource is Border evtborder)
                    {
                        var keystores = FindVisualChildren<KeyStoreControl>(evtborder);
                        Console.WriteLine($"evtborder.tag: {evtborder.Tag}");
                        var keyToSend = keystores.FirstOrDefault();
                        if (keyToSend != null)
                        {
                            SendInputs.SendKeyPress(keyToSend.Key);
                        }
                    }
                };
            }

            for (int i = 0; i < Items.Count; i++)
            {
                UIElement uiElement =
                    (UIElement)ItemContainerGenerator.ContainerFromIndex(i);

                if (uiElement is ContentControl cc)
                {
                    cc.ApplyTemplate();
                    var t = cc.ContentTemplate;

                    System.Diagnostics.Debug.WriteLine("");
                }

                if (uiElement is ContentPresenter cp && cp.Content is Key1)
                {
                    var result = cp.ApplyTemplate();
                    var template = cp.ContentTemplate;

                    System.Diagnostics.Debug.WriteLine("");
                }
            }

            var keys = GetLogicalChildren<Key1>(this);

            bool first = true;
            bool newline = false;
            int previousKeyLeft = -1;
            int previousKeyTop = -1;
            int currentHorizontalSpacing = -1;
            foreach (var key in keys)
            {
                if (key.HorizontalSpacing > 0)
                {
                    currentHorizontalSpacing = key.HorizontalSpacing;
                }
                if (first)
                {
                    key.Left = 0;
                    key.Top = 0;
                    first = false;
                }
                else if (newline)
                {
                    key.Left = 0;
                    key.Top = previousKeyTop + key.VerticalSpacing;
                    newline = false;
                }
                else
                {
                    key.Left = previousKeyLeft + currentHorizontalSpacing;
                }
                previousKeyLeft = key.Left;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
