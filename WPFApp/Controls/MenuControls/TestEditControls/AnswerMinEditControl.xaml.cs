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
    /// Interaction logic for AnswerMinEditControl.xaml
    /// </summary>
    public partial class AnswerMinEditControl : UserControl
    {
        public bool IsSelected
        {
            get
            {
                return CtrlBorder.BorderBrush == (Brush)FindResource("CardBorderColor");
            }
            set
            {
                IsTabStop = Focusable = !value;

                if (value)
                    CtrlBorder.BorderBrush = (Brush)FindResource("CardBorderColor");
                else
                    CtrlBorder.BorderBrush = CtrlBorder.Background;
            }
        }
        public bool IsCorrect
        {
            get
            {
                return CtrlBorder.Background == (Brush)FindResource("PublicColor");
            }
            set
            {
                CtrlBorder.Background = (Brush)FindResource(value ? "PublicColor" : "PrivateColor");
                IsSelected = IsSelected;
            }
        }
        public bool IsRadio
        {
            get
            {
                return CtrlIsCorrectRadio.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    if (CtrlIsCorrectCheck.IsChecked == true)
                    {
                        CtrlIsCorrectRadio.IsChecked = true;
                        CtrlIsCorrectCheck.IsChecked = false;
                    }
                    CtrlIsCorrectRadio.Visibility = Visibility.Visible;
                    CtrlIsCorrectCheck.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (CtrlIsCorrectRadio.IsChecked == true)
                        CtrlIsCorrectCheck.IsChecked = true;
                    CtrlIsCorrectRadio.Visibility = Visibility.Collapsed;
                    CtrlIsCorrectCheck.Visibility = Visibility.Visible;
                }
            }
        }
        public bool IsChecked
        {
            get
            {
                if (IsRadio)
                    return CtrlIsCorrectRadio.IsChecked == true;
                else
                    return CtrlIsCorrectCheck.IsChecked == true;
            }
            set
            {
                CtrlIsCorrectRadio.IsChecked = CtrlIsCorrectCheck.IsChecked = value;
            }
        }
        public AnswerInfo Answer
        {
            get
            {
                if (answer == null)
                    answer = new AnswerInfo();

                answer.IsCorrect = (IsRadio ? CtrlIsCorrectRadio.IsChecked == true : CtrlIsCorrectCheck.IsChecked == true);

                return answer;
            }
            set
            {
                answer = value;

                if(value == null)
                {
                    CtrlText.Text = string.Empty;

                    CtrlIsCorrectRadio.IsChecked = 
                        CtrlIsCorrectCheck.IsChecked = false;
                    return;
                }

                CtrlText.Text = value.Text;

                CtrlIsCorrectRadio.IsChecked = 
                    CtrlIsCorrectCheck.IsChecked = value.IsCorrect;
            }
        }
        public string Text
        {
            get
            {
                return CtrlText.Text;
            }
            set
            {
                if (answer == null)
                    answer = new AnswerInfo();
                CtrlText.Text = value;
                answer.Text = value;
            }
        }

        AnswerInfo answer;

        public AnswerMinEditControl(bool isRadio = false, AnswerInfo answer = null)
        {
            InitializeComponent();
            IsRadio = isRadio;
            Answer = answer;
        }

        #region Click

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            AppManager.Instance.QuestionEditControl.RemoveAnswer(this);
        }

        private void CtrlIsCorrectRadio_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        #endregion
    }
}
