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
using WPFApp.Controls.MenuControls.CatalogControls;
using WPFApp.Controls.MenuControls.InvitationsControls;

namespace WPFApp.Controls.MenuControls
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public string Title
        {
            get
            {
                return selectedButton.Content.ToString();
            }
        }
        AppManager manager;
        Dictionary<string, UserControl> elements;
        Button selectedButton;
        public event Action Updated;
        
        public MenuControl()
        {
            InitializeComponent();
            manager = AppManager.Instance;
        }

        private void ButtonSelectPage_Click(object sender, RoutedEventArgs e)
        {
            selectedButton.IsEnabled = true;

            selectedButton = sender as Button;
            selectedButton.IsEnabled = false;

            CtrlPage.Child = elements[Title];

            manager.WindowTitle = Title;
        }

        void UpdatePageButtons()
        {
            CtrlPages.Children.Clear();

            Button btn;
            foreach (KeyValuePair<string, UserControl> element in elements)
            {
                btn = new Button()
                {
                    Content = element.Key,
                    Style = (Style)FindResource("PageTabButton")
                };
                btn.Click += ButtonSelectPage_Click;

                CtrlPages.Children.Add(btn);
            }
        }

        public void Update()
        {
            Updated?.Invoke();
        }

        public void Use(Dictionary<string, UserControl> elements)
        {
            Updated = null;
            this.elements = elements;

            UpdatePageButtons();

            selectedButton = CtrlPages.Children[0] as Button;
            ButtonSelectPage_Click(selectedButton, null);
        }

        public void Back()
        {
            manager.CurControl = this;
            manager.WindowTitle = Title;
            Update();
        }
    }
}
