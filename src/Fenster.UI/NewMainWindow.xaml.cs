using System.Windows;
using System.Windows.Controls.Primitives;
using Point = System.Windows.Point;

namespace Fenster.UI
{
    /// <summary>
    /// Interaction logic for NewMainWindow.xaml
    /// </summary>
    public partial class NewMainWindow : Window
    {
        public NewMainWindow()
        {
            InitializeComponent();

            ContextMenuItem.Click += ContextMenuItem_Click;
            //this.Popup.Loaded += new RoutedEventHandler(Window_Loaded);

        }

        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;

            Popup.Placement = PlacementMode.Absolute;

            Popup.HorizontalOffset = desktopWorkingArea.Right - this.Popup.Width;
            Popup.VerticalOffset = desktopWorkingArea.Bottom - this.Popup.Height;
        }
    }
}
