using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows.Navigation;

namespace PressJsonToJson
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HelpWindow : MetroWindow
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
