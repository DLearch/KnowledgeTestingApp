using ContractLib.TestComponents;
using ContractLib.TestComponents.QuestionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for TestEditControl.xaml
    /// </summary>
    public partial class TestEditControl : UserControl
    {
        public int? TestId { get; set; }
        public TimeSpan? Duration
        {
            get
            {
                int tmp;

                if (int.TryParse(CtrlDuration.Text, out tmp))
                    return new TimeSpan(0, tmp, 0);

                return null;
            }
            set
            {
                if (value.HasValue)
                    CtrlDuration.Text = value.Value.Minutes.ToString();
                else
                    CtrlDuration.Text = string.Empty;
            }
        }
        public TimeSpan? Interval
        {
            get
            {
                DateTime dt;

                if (DateTime.TryParseExact(CtrlInterval.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    return dt.TimeOfDay;
                
                return null;
            }
            set
            {
                if (value.HasValue)
                    CtrlDuration.Text = value.Value.ToString(@"hh\:mm");
                else
                    CtrlDuration.Text = string.Empty;
            }
        }
        public int? Attempts
        {
            get
            {
                int attempts;

                if (int.TryParse(CtrlAttempts.Text, out attempts))
                    return attempts;

                return null;
            }
            set
            {
                if (value.HasValue)
                    CtrlAttempts.Text = value.ToString();
                else
                    CtrlAttempts.Text = string.Empty;
            }
        }
        public List<QuestionInfo> Questions
        {
            get
            {
                List<QuestionInfo> questions = new List<QuestionInfo>();

                foreach (QuestionMinEditControl item in CtrlQuestions.Children)
                    questions.Add(item.Question);
                
                return questions;
            }
            set
            {
                CtrlQuestions.Children.Clear();
                if (value == null || value.Count == 0)
                {
                    CtrlErrorQuestions.Visibility = Visibility.Visible;
                    return;
                }

                CtrlErrorQuestions.Visibility = Visibility.Collapsed;

                foreach (QuestionInfo item in value)
                    CtrlQuestions.Children.Add(new QuestionMinEditControl(item));
            }
        }
        public TestInfo Test
        {
            get
            {
                TestInfo test = new TestInfo()
                {
                    IsPrivate = CtrlIsPrivate.IsChecked == true,
                    Category = (KeyValuePair<int, string>)CtrlCategories.SelectedItem,
                    RatingSystem = (KeyValuePair<int, string>)CtrlRatingSystems.SelectedItem,
                    Title = CtrlTitle.Text,
                    Description = CtrlDescription.Text,
                    IsQuestionsMix = CtrlMixQuestions.IsChecked == true,
                    Duration = Duration,
                    Interval = Interval,
                    Attempts = Attempts
                };

                if (TestId.HasValue)
                    test.Id = TestId.Value;

                return test;
            }
            set
            {
                UpdateCtrlCategories();
                UpdateCtrlRatingSystems();

                if (value == null)
                {
                    TestId = null;

                    CtrlTitle.Text =
                        CtrlDescription.Text = string.Empty;

                    Duration = Interval = null;
                    Attempts = null;

                    CtrlIsPrivate.IsChecked =
                        CtrlMixQuestions.IsChecked = false;

                    Questions = null;
                    manager.QuestionEditControl = new QuestionEditControl();

                    return;
                }

                TestId = value.Id;
                CtrlTitle.Text = value.Title;
                CtrlDescription.Text = value.Description;
                CtrlIsPrivate.IsChecked = value.IsPrivate;
                CtrlMixQuestions.IsChecked = value.IsQuestionsMix;
                Questions = manager.Channel.GetQuestions(value.Id);

                SelectIndexComboBox(CtrlCategories, value.Category.Key);
                SelectIndexComboBox(CtrlRatingSystems, value.RatingSystem.Key);

                Attempts = value.Attempts;
                Interval = value.Interval;
                Duration = value.Duration;
            }
        }
        public string Title
        {
            get
            {
                return (TestId == null ? "Добавление" : "Редактирование") + " теста";
            }
        }

        AppManager manager;

        public TestEditControl(int? testId = null)
        {
            InitializeComponent();

            manager = AppManager.Instance;

            UpdateCtrlCategories();
            UpdateCtrlRatingSystems();
        }

        #region Click

        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            manager.QuestionEditControl.Use();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAllFields())
                return;
            
            TestInfo test = Test;


            if (!TestValidator.IsValid(test))
            {
                CtrlError.ShowError(TestValidator.Error);
                return;
            }

            if (TestId != null)
                manager.Channel.EditTest(test, Questions);
            else
                manager.Channel.AddTest(test, Questions);

            manager.MenuControl.Back();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MenuControl.Back();
        }
        #endregion
        #region PreviewTextInput

        private void CtrlAttempts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
        private void CtrlDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void CtrlInterval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = char.IsLetter(e.Text, 0);
        }
        #endregion
        #region KeyDown

        private void CtrlTitle_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorTitle.ClearError();
        }
        private void CtrlDescription_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorDescription.ClearError();
        }
        private void CtrlDuration_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorDuration.ClearError();
        }
        private void CtrlInterval_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorInterval.ClearError();
        }
        #endregion
        #region LostFocus

        private void CtrlTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTitle();
        }
        private void CtrlDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckDescription();
        }
        private void CtrlDuration_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckDuration();
        }
        private void CtrlInterval_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckInterval();
        }
        #endregion
        #region CheckAllFields(), CheckTitle(), CheckDescription(), CheckDuration(), CheckInterval()

        public bool CheckAllFields()
        {
            return CheckTitle()
                & CheckDescription()
                & CheckDuration()
                & CheckInterval();
        }

        public bool CheckTitle()
        {
            if (!TestValidator.IsValidTitle(CtrlTitle.Text))
            {
                CtrlErrorTitle.ShowError(TestValidator.Error);
                return false;
            }

            if (!manager.Channel.TestTitleIsAvailable(CtrlTitle.Text, TestId))
            {
                CtrlErrorTitle.ShowError("Название занято.");
                return false;
            }

            CtrlErrorTitle.ClearError();
            CtrlError.ClearError();
            return true;
        }
        public bool CheckDescription()
        {
            if (!TestValidator.IsValidDescription(CtrlDescription.Text))
            {
                CtrlErrorDescription.ShowError(TestValidator.Error);
                return false;
            }

            CtrlErrorDescription.ClearError();
            CtrlError.ClearError();
            return true;
        }
        public bool CheckDuration()
        {
            int minutes;
            if (!string.IsNullOrEmpty(CtrlDuration.Text) && !int.TryParse(CtrlDuration.Text, out minutes))
            {
                CtrlErrorDuration.ShowError("Неправильный формат.");
                return false;
            }

            CtrlErrorDuration.ClearError();
            CtrlError.ClearError();
            return true;
        }
        public bool CheckInterval()
        {
            DateTime dtime;
            if (!string.IsNullOrEmpty(CtrlInterval.Text) && !DateTime.TryParseExact(CtrlInterval.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtime))
            {
                CtrlErrorInterval.ShowError("Неправильный формат.");
                return false;
            }

            CtrlErrorInterval.ClearError();
            CtrlError.ClearError();
            return true;
        }

        #endregion
        #region UpdateCtrlCategories(), UpdateCtrlRatingSystems(), SelectIndexComboBox(-)

        void UpdateCtrlCategories()
        {
            CtrlCategories.ItemsSource = manager.Channel.GetCategories();

            if((CtrlCategories.ItemsSource as ICollection).Count > 0)
                CtrlCategories.SelectedIndex = 0;
        }
        void UpdateCtrlRatingSystems()
        {
            CtrlRatingSystems.ItemsSource = manager.Channel.GetRatingSystems();

            if ((CtrlRatingSystems.ItemsSource as ICollection).Count > 0)
                CtrlRatingSystems.SelectedIndex = 0;
        }

        void SelectIndexComboBox(ComboBox box, int elementId)
        {
            box.SelectedItem = (box.ItemsSource as Dictionary<int, string>).FirstOrDefault(c => c.Key == elementId);
        }
        #endregion
        #region Use(-), Back(), AddQuestion(-)

        public void Use(int? testId = null)
        {
            if (testId.HasValue)
                Test = manager.Channel.GetTest(testId.Value);
            else
                Test = null;

            Back();
        }

        public void Back()
        {
            manager.CurControl = this;
            manager.WindowTitle = Title;
        }

        public void AddQuestion(QuestionInfo question)
        {
            CtrlErrorQuestions.Visibility = Visibility.Collapsed;
            
            CtrlQuestions.Children.Add(new QuestionMinEditControl(question));
        }
        public void EditQuestion(QuestionInfo question)
        {
            CtrlErrorQuestions.Visibility = Visibility.Collapsed;

            foreach (QuestionMinEditControl item in CtrlQuestions.Children)
                if (item.Question.Id == question.Id)
                    item.Question = question;
        }

        public void RemoveQuestion(QuestionMinEditControl qmec)
        {
            CtrlQuestions.Children.Remove(qmec);

            if (CtrlQuestions.Children.Count == 0)
                CtrlErrorQuestions.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
