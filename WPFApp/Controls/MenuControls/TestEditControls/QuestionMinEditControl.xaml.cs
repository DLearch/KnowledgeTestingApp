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
    /// Interaction logic for QuestionMinEditControl.xaml
    /// </summary>
    public partial class QuestionMinEditControl : UserControl
    {
        QuestionInfo question;
        AppManager manager;
        #region Question

        public QuestionInfo Question
        {
            get
            {
                int weight = 0;
                if (int.TryParse(CtrlWeight.Text, out weight))
                    question.Weight = weight;

                return question;
            }
            set
            {
                CtrlText.Text = string.Empty;

                question = value;

                if (value != null)
                {
                    CtrlText.Text = value.Text;
                    CtrlWeight.Text = value.Weight.ToString();
                }
            }
        }
        #endregion

        public QuestionMinEditControl(QuestionInfo question = null)
        {
            InitializeComponent();
            manager = AppManager.Instance;
            Question = question;
        }

        #region Click

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            manager.QuestionEditControl.Use(Question);
        }
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            manager.TestEditControl.RemoveQuestion(this);
        }
        #endregion

        private void CtrlWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}
