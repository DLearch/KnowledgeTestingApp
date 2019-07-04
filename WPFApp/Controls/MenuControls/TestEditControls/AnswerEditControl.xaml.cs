using ContractLib.TestComponents.QuestionComponents;
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

namespace WPFApp.Controls.MenuControls.TestEditControls
{
    /// <summary>
    /// Interaction logic for AnswerEditControl.xaml
    /// </summary>
    public partial class AnswerEditControl : UserControl
    {
        AnswerMinEditControl amec;
        public AnswerMinEditControl AnswerMinEditControl
        {
            get
            {
                return amec;
            }
            set
            {

                CtrlEmpty.Visibility =
                    CtrlContent.Visibility =
                    CtrlLoadImage.Visibility =
                    CtrlRemoveImage.Visibility = Visibility.Collapsed;

                amec = value;

                if (value == null)
                {
                    CtrlEmpty.Visibility = Visibility.Visible;
                    CtrlText.Text = string.Empty;
                    CtrlImage.Source = null;
                    return;
                }
                
                CtrlContent.Visibility = 
                    CtrlLoadImage.Visibility = Visibility.Visible;

                CtrlText.Text = value.Text;
                CtrlImage.Source = AppManager.GetBitmapImage(value.Answer.Image);

                if (CtrlImage.Source != null)
                    CtrlRemoveImage.Visibility = Visibility.Visible;
            }
        }
        AppManager manager;
        public AnswerEditControl()
        {
            InitializeComponent();
            AnswerMinEditControl = null;
            manager = AppManager.Instance;
        }

        #region KeyDown, LostFocus

        private void CtrlText_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorText.ClearError();
        }

        private void CtrlText_LostFocus(object sender, RoutedEventArgs e)
        {
            amec.Text = CtrlText.Text;
            CheckName();
        }
        #endregion
        #region CheckName()

        public bool CheckName()
        {
            if (!AnswerValidator.IsValidText(CtrlText.Text))
            {
                CtrlErrorText.ShowError(AnswerValidator.Error);
                return false;
            }

            CtrlErrorText.ClearError();
            return true;
        }
        #endregion

        private void CtrlText_KeyUp(object sender, KeyEventArgs e)
        {
            amec.Text = CtrlText.Text;
        }

        private void CtrlRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            amec.Answer.Image = null;
            CtrlImage.Source = null;
            CtrlRemoveImage.Visibility = Visibility.Hidden;
        }

        private void ButtonLoadImage_Click(object sender, RoutedEventArgs e)
        {
            manager.CurMessageControl = manager.LoadImageControl = new LoadImageControl();
            manager.LoadImageControl.ImageLoaded += LoadImage;
        }

        public void LoadImage()
        {
            amec.Answer.Image = manager.LoadImageControl.Image;
            CtrlImage.Source = AppManager.GetBitmapImage(manager.LoadImageControl.Image);
            if (CtrlImage.Source != null)
                CtrlRemoveImage.Visibility = Visibility.Visible;
        }
    }
}
