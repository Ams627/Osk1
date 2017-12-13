using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Osk1
{

    public class QQKey1 
    {
        /// <summary>
        /// Left margin for the key. Each successive key in a KeyboardItemsContainer will add Horizontal Spacing to
        /// this figure - if you leave it at its default (-1), it will be zero for the first key.
        /// </summary>
        public int Left { get; set; } = -1;
        public int Top { get; set; } = -1;
        public string Label { get; set; }
        public int HorizontalSpacing { get; set; } = -1;
        public int VerticalSpacing { get; set; }
        public object Tag { get; set; }
        public Thickness Margin => new Thickness(Left, Top, 0, 0);
        public bool NewLine { get; set; }
        public SendInputs.KeyCode Key { get; set; }
        public ModifierKeys Modifiers { get; set; }

    }
}
