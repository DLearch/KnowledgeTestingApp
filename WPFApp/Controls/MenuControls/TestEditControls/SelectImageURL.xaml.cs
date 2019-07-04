using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for SelectImageURL.xaml
    /// </summary>
    public partial class SelectImageURL : UserControl
    {
        public SelectImageURL()
        {
            InitializeComponent();
        }
        
        private void CtrlLoad_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckURL())
                return;

            byte[] photobytes = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    photobytes = client.DownloadData(CtrlURL.Text);
                }
            }
            catch (Exception)
            {
                CtrlError.ShowError("Ошибка.");
            }

            AppManager.Instance.LoadImageControl.LoadImage(photobytes);
        }

        private void CtrlURL_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlError.ClearError();
        }

        private void CtrlURL_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckURL();
        }

        bool CheckURL()
        {
            if (string.IsNullOrEmpty(CtrlURL.Text))
            {
                CtrlError.ShowError("Укажите URL изображения.");
                return false;
            }
            CtrlError.ClearError();
            return true;
        }
    }
}
