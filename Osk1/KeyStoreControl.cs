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
    public class KeyStoreControl : Control
    {


        public SendInputs.KeyCode Key
        {
            get { return (SendInputs.KeyCode )GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(SendInputs.KeyCode ), typeof(KeyStoreControl), new PropertyMetadata(null));


        public bool IsAltPressed { get; set; }
        public bool IsCtrlPressed { get; set; }
        static KeyStoreControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyStoreControl), new FrameworkPropertyMetadata(typeof(KeyStoreControl)));
        }
    }
}
