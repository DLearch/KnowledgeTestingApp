using ContractLib.TestComponents.QuestionComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for QuestionEditControl.xaml
    /// </summary>
    public partial class QuestionEditControl : UserControl
    {
        public QuestionInfo Question
        {
            get
            {
                QuestionInfo question = new QuestionInfo()
                {
                    Text = CtrlText.Text,
                    IsRadio = CtrlIsRadio.IsChecked == true,
                    IsAnswersMix = CtrlMixAnswers.IsChecked == true,
                    Answers = Answers,
                    Weight = weight,
                    Image = image
                };
                
                if (questionId.HasValue)
                    question.Id = questionId.Value;

                return question;
            }
            set
            {
                selectedCard = null;
                if (value == null)
                {
                    questionId = null;
                    CtrlText.Text = string.Empty;
                    weight = 1;
                    CtrlIsRadio.IsChecked = 
                        CtrlMixAnswers.IsChecked = false;
                    Answers = null;
                    CtrlEditAnswer.CtrlText.Text = string.Empty;
                    image = null;
                    return;
                }
                questionId = value.Id;
                weight = value.Weight;
                if (weight < 1)
                    weight = 1;
                CtrlIsRadio.IsChecked = value.IsRadio;
                CtrlText.Text = value.Text;
                CtrlMixAnswers.IsChecked = value.IsAnswersMix;
                Answers = value.Answers;
                image = value.Image;
                CtrlImg.Source = AppManager.GetBitmapImage(image);
                if (CtrlImg.Source != null)
                    CtrlRemoveImage.Visibility = Visibility.Visible;
                else
                    CtrlRemoveImage.Visibility = Visibility.Hidden;
            }
        }
        public List<AnswerInfo> Answers
        {
            get
            {
                List<AnswerInfo> answers = new List<AnswerInfo>();

                foreach (AnswerMinEditControl item in CtrlAnswers.Children)
                    answers.Add(item.Answer);
                return answers;
            }
            set
            {
                selectedCard = null;
                CtrlAnswers.Children.Clear();

                if (value == null || value.Count == 0)
                {
                    CtrlErrorAnswers.Visibility = Visibility.Visible;
                    CtrlEditAnswer.AnswerMinEditControl = null;
                    return;
                }

                CtrlErrorAnswers.Visibility = Visibility.Collapsed;

                foreach (AnswerInfo item in value)
                    AddAnswer(item);
            }
        }
        public string Title
        {
            get
            {
                return (questionId == null ? "Добавление" : "Редактирование") + " вопроса";
            }
        }

        byte[] image;
        int? questionId;
        int weight;
        AppManager manager;
        AnswerMinEditControl selectedCard;

        public QuestionEditControl(QuestionInfo question = null)
        {
            InitializeComponent();

            manager = AppManager.Instance;

            Question = question;
            selectedCard = null;

        }
        #region Click

        private void ButtonRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            CtrlImg.Source = null;
            CtrlRemoveImage.Visibility = Visibility.Hidden;
        }
        private void ButtonLoadImage_Click(object sender, RoutedEventArgs e)
        {
            manager.CurMessageControl = manager.LoadImageControl = new LoadImageControl();
            manager.LoadImageControl.ImageLoaded += LoadImage;
        }
        private void CtrlAnswerCard_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCard != null)
            {
                selectedCard.IsSelected = false;
                selectedCard.Text = CtrlEditAnswer.CtrlText.Text;
            }
            selectedCard = sender as AnswerMinEditControl;
            selectedCard.IsSelected = true;
            CtrlEditAnswer.AnswerMinEditControl = selectedCard;
        }
        private void CtrlIsRadio_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnswerMinEditControl item in CtrlAnswers.Children)
                item.IsRadio = CtrlIsRadio.IsChecked == true;
        }

        private void CtrlAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            AddAnswer();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckText())
                return;

            QuestionInfo question = Question;

            if (!QuestionValidator.IsValid(question))
            {
                CtrlError.ShowError(QuestionValidator.Error);
                return;
            }

            if(questionId != null)
                manager.TestEditControl.EditQuestion(Question);
            else
                manager.TestEditControl.AddQuestion(Question);

            manager.TestEditControl.Back();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            manager.TestEditControl.Back();
        }
        #endregion
        #region CheckText(), AddAnswer(-), RemoveAnswer(-)

        public void AddAnswer(AnswerInfo answer = null)
        {
            CtrlErrorAnswers.Visibility = Visibility.Collapsed;
            AnswerMinEditControl amec = new AnswerMinEditControl(CtrlIsRadio.IsChecked == true, answer);
            amec.MouseLeftButtonUp += CtrlAnswerCard_Click;
            CtrlAnswers.Children.Add(amec);
        }

        public void RemoveAnswer(AnswerMinEditControl amec)
        {
            if (amec == selectedCard)
            {
                selectedCard = null;
                CtrlEditAnswer.AnswerMinEditControl = null;
            }
            CtrlAnswers.Children.Remove(amec);

            if(CtrlAnswers.Children.Count == 0)
                CtrlErrorAnswers.Visibility = Visibility.Visible;
        }

        bool CheckText()
        {
            if (!QuestionValidator.IsValidText(CtrlText.Text))
            {
                CtrlErrorText.ShowError(QuestionValidator.Error);
                return false;
            }

            CtrlErrorText.ClearError();
            return true;
        }
        #endregion
        #region KeyDown, LostFocus
        
        private void CtrlText_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorText.ClearError();
            CtrlError.ClearError();
        }

        private void CtrlText_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckText();
        }
        #endregion
        #region Use(-), Back(), LoadImage()

        public void Use(QuestionInfo question = null)
        {
            Back();

            Question = question;

        }

        public void Back()
        {
            manager.CurControl = this;
            manager.WindowTitle = Title;
        }
        public void LoadImage()
        {
            CtrlImg.Source = AppManager.GetBitmapImage(manager.LoadImageControl.Image);
            if(CtrlImg.Source != null)
                CtrlRemoveImage.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
