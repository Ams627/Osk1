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
        public KeyboardItemsControl()
        {
            this.Loaded += KeyboardItemsControl_Loaded;

            // set the panel type for the items control to grid:
            FrameworkElementFactory factoryPanel = new FrameworkElementFactory(typeof(Grid));
            factoryPanel.SetValue(StackPanel.IsItemsHostProperty, true);
            ItemsPanelTemplate template = new ItemsPanelTemplate
            {
                VisualTree = factoryPanel
            };

            ItemsPanel = template;
        }

        public bool DefaultAlignment
        {
            get { return (bool)GetValue(DefaultAlignmentProperty); }
            set { SetValue(DefaultAlignmentProperty, value); }
        }

        public static readonly DependencyProperty DefaultAlignmentProperty =
            DependencyProperty.Register("DefaultAlignment", typeof(bool), typeof(KeyboardItemsControl), new PropertyMetadata(true));



        public bool DefaultSize
        {
            get { return (bool)GetValue(DefaultSizeProperty); }
            set { SetValue(DefaultSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultSizeProperty =
            DependencyProperty.Register("DefaultSize", typeof(bool), typeof(KeyboardItemsControl), new PropertyMetadata(true));

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

        private void KeyboardItemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            //            Utils.PrintChildren("Loaded", this);


            //var children = FindVisualChildren<DependencyObject>(this);
            //foreach (var child in children)
            //{
            //    if (child is FrameworkElement f)
            //    {
            //        var margin = f.Margin;
            //        var tag = f.Tag;
            //        string tagDebug = tag == null ? "" : $" tag: {tag.ToString()}";
            //        System.Diagnostics.Debug.WriteLine($"Type {f.GetType()}, Margin {margin.Left},{margin.Top},{margin.Right},{margin.Bottom} {tagDebug}");
            //    }
            //}

            var borders = FindVisualChildren<Border>(this);

            foreach (var border in borders)
            {
                if (border.Tag is string str && (str == "KeyInner" || str == "KeyOuter"))
                {
                    if (double.IsNaN(border.Height) || border.Height <= 0)
                    {
                        border.Height = 50;
                    }

                    if (double.IsNaN(border.Width) || border.Width <= 0)
                    {
                        border.Width = 50;
                    }

                    if (DefaultAlignment)
                    {
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        border.VerticalAlignment = VerticalAlignment.Top;
                    }

                    if (str == "KeyInner")
                    {
                        if (border.ReadLocalValue(BackgroundProperty) == DependencyProperty.UnsetValue)
                        {
                            border.Background = Brushes.Transparent;
                        }

                        border.MouseLeftButtonDown += (mouseSender, eventargs) =>
                        {
                            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.Ticks % 1000000}");
                            var type = mouseSender.GetType();
                            if (mouseSender is Border evtborder)
                            {
                                var keystores = FindVisualChildren<KeyStoreControl>(evtborder);
                                if (keystores.Count() > 1)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Keystores > 1");
                                }
                                var keyToSend = keystores.FirstOrDefault();
                                System.Diagnostics.Debug.WriteLine($"evtborder.tag: {evtborder.Tag} {keyToSend.Key}");
                                if (keyToSend != null)
                                {
                                    SendInputs.SendKeyPress(keyToSend.Key);
                                }
                            }
                        };
                    }
                    else if (str == "KeyOuter")
                    {
                        if (border.ReadLocalValue(BorderBrushProperty) == DependencyProperty.UnsetValue)
                        {
                            border.BorderBrush = Brushes.CadetBlue;
                        }
                    }
                }
            }
        }
    

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var keys = GetLogicalChildren<Key1>(this);

            bool first = true;
            int previousKeyLeft = -1;
            int previousKeyTop = -1;
            int currentHorizontalSpacing = -1;
            int currentVerticalSpacing = 0;
            foreach (var key in keys)
            {
                if (key.HorizontalSpacing > 0)
                {
                    currentHorizontalSpacing = key.HorizontalSpacing;
                }
                if (key.VerticalSpacing > 0)
                {
                    currentVerticalSpacing = key.VerticalSpacing;
                }
                if (first)
                {
                    if (key.Left == -1)
                    {
                        key.Left = 0;
                    }
                    if (key.Top == -1)
                    {
                        key.Top = 0;
                    }
                    first = false;
                }
                else if (key.NewLine)
                {
                    if (key.Left == -1)
                    {
                        key.Left = 0;
                    }
                    if (key.Top == -1)
                    {
                        key.Top = previousKeyTop + currentVerticalSpacing;
                    }
                    previousKeyTop = key.Top;
                }
                else
                {
                    key.Left = previousKeyLeft + currentHorizontalSpacing;
                    key.Top = previousKeyTop;
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
