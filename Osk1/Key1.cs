using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Osk1
{
    public class Key1 
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public string Label { get; set; }
        public int HorizontalSpacing { get; set; } = -1;
        public int VerticalSpacing { get; set; }
        public Thickness Margin => new Thickness(Left, Top, 0, 0);
        public bool NewLine { get; set; }
        public Key Key { get; set; }
        public ModifierKeys Modifiers { get; set; }

        private static void Handler(object sender, EventArgs e)
        {

        }
    }
}
