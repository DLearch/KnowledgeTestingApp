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

namespace WPFApp.Controls.AdditionalControls
{
    /// <summary>
    /// Interaction logic for ErrorControl.xaml
    /// </summary>
    public partial class ErrorControl : UserControl
    {
        #region ControlProperty
        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register("Control", typeof(Control), typeof(ErrorControl));

        public Control Control
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        #endregion
        #region ControlsProperty
        public static readonly DependencyProperty ControlsProperty = DependencyProperty.Register("Controls", typeof(List<Control>), typeof(ErrorControl));

        public List<Control> Controls
        {
            get { return (List<Control>)GetValue(ControlsProperty); }
            set { SetValue(ControlsProperty, value); }
        }

        #endregion

        public ErrorControl()
        {
            InitializeComponent();
            ClearError();
        }

        #region ShowError(string error), ClearError()
        public void ShowError(string error)
        {
            TextBlockError.Text = error;
            Height = 30;
            if (Control != null)
                Control.BorderBrush = Brushes.Red;
            if (Controls != null)
                foreach (var item in Controls)
                    item.BorderBrush = Brushes.Red;
        }

        public void ClearError()
        {
            TextBlockError.Text = string.Empty;
            Height = 0;
            if (Control != null)
                Control.BorderBrush = Brushes.LightGray;
            if (Controls != null)
                foreach (var item in Controls)
                    item.BorderBrush = Brushes.LightGray;
        }
        #endregion
    }
}
