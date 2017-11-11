using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Osk1
{
    public class KeyboardItemsControl1 : ItemsControl
    {
        static KeyboardItemsControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
  typeof(KeyboardItemsControl1),
  new FrameworkPropertyMetadata(typeof(KeyboardItemsControl1)));
        }
        public KeyboardItemsControl1()
        {
            this.DefaultStyleKey = typeof(KeyboardItemsControl1);
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
