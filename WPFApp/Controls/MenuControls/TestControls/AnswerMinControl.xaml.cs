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

namespace WPFApp.Controls.MenuControls.TestControls
{
    /// <summary>
    /// Interaction logic for AnswerMinControl.xaml
    /// </summary>
    public partial class AnswerMinControl : UserControl
    {
        public AnswerInfo Answer
        {
            get
            {
                if (CtrlIsCorrectRadio.Visibility == Visibility.Visible)
                    answer.IsCorrect = CtrlIsCorrectRadio.IsChecked == true;
                else
                    answer.IsCorrect = CtrlIsCorrectCheck.IsChecked == true;

                return answer;
            }
        }
        AnswerInfo answer;
        public AnswerMinControl(AnswerInfo answer, bool? isRadio = null)
        {
            InitializeComponent();

            CtrlText.Text = answer.Text;

            if (isRadio == true)
                CtrlIsCorrectCheck.Visibility = Visibility.Collapsed;
            else
                CtrlIsCorrectRadio.Visibility = Visibility.Collapsed;
            
            CtrlImage.Source = AppManager.GetBitmapImage(answer.Image);

            this.answer = answer;
        }
    }
}
