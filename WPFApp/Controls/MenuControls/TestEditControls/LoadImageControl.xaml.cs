using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for LoadImageControl.xaml
    /// </summary>
    public partial class LoadImageControl : UserControl
    {
        public event Action ImageLoaded;
        public byte[] Image { get; set; }
        AppManager manager;
        public LoadImageControl()
        {
            InitializeComponent();
            manager = AppManager.Instance;
            MenuControl menuControl = new MenuControl();

            CtrlMenu.Child = menuControl;

            Dictionary<string, UserControl> elements = new Dictionary<string, UserControl>();

            elements.Add("URL", new SelectImageURL());
            elements.Add("Файл", new SelectImageFile());

            menuControl.Use(elements);
        }

        public void LoadImage(byte[] img)
        {
            Image = img;
            if (img == null || img.Length == 0) return;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(img))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            CtrlImg.Source = image;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            AppManager.Instance.CurMessageControl = null;
        }

        private void CtrlSave_Click(object sender, RoutedEventArgs e)
        {
            ImageLoaded?.Invoke();
            AppManager.Instance.CurMessageControl = null;
        }
    }
}
