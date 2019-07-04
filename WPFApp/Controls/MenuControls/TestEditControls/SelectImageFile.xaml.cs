using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPFApp.Controls.MenuControls.TestEditControls
{
    /// <summary>
    /// Interaction logic for SelectImageFile.xaml
    /// </summary>
    public partial class SelectImageFile : UserControl
    {
        OpenFileDialog ofd;
        public SelectImageFile()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            if (ofd.ShowDialog() == true)
            {
                CtrlFileName.Content = ofd.FileName;
                CtrlLoad.IsEnabled = true;
            }
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            AppManager.Instance.LoadImageControl.LoadImage(File.ReadAllBytes(ofd.FileName));
        }
    }
}
