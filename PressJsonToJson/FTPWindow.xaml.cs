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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace PressJsonToJson
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FTPWindow : MetroWindow
    {
        public FTP ftp;
        public List<string> mapsList;
        public List<string> typesList;

        public FTPWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ftp = new FTP(txt_ftpAddress.Text, txt_ftpUsername.Text, txt_ftpPassword.Password, cb_FTPPassive.IsChecked.Value);
            mapsList = ftp.GetFoldersOverFTP(ftp.ftpDetails, "maps");
            typesList = ftp.GetFoldersOverFTP(ftp.ftpDetails, "variants");
        }
    }
}
