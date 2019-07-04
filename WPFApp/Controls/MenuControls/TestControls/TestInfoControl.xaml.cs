using ContractLib.TestComponents;
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
using WPFApp.Controls.MenuControls.InvitationsControls;

namespace WPFApp.Controls.MenuControls.TestControls
{
    /// <summary>
    /// Interaction logic for TestInfoControl.xaml
    /// </summary>
    public partial class TestInfoControl : UserControl
    {
        #region testId, TestId
        int? testId;

        public int? TestId
        {
            get
            {
                return testId;
            }
            set
            {
                Clear();

                testId = value;

                if (value == null)
                    return;

                TestInfo test = manager.Channel.GetTest(testId.Value);

                List<string> infoList = new List<string>();

                CtrlTitle.Text = test.Title;
                CtrlDescription.Text = test.Description;

                if (test.User.Id != manager.User.Id)
                    if (!string.IsNullOrEmpty(test.Mark))
                        infoList.Add("Оценка: " + test.Mark);
                    else
                        infoList.Add("Вы ещё не сдавали этот тест.");

                infoList.Add("Количество вопросов: " + test.QuestionsCount);

                if (test.Attempts != null && test.UsedAttempts > 0)
                    infoList.Add("Использовано попыток: " + test.UsedAttempts + " из " + test.Attempts);
                else if (test.Attempts == null)
                    infoList.Add("Неограниченое количество попыток.");
                else
                    infoList.Add("Попыток: " + test.Attempts);

                if (test.Duration != null)
                    infoList.Add("Время для сдачи теста: " + test.Duration.Value.ToString(@"hh\:mm"));
                else
                    infoList.Add("Время для сдачи теста неограничено.");

                if (test.Interval != null)
                    infoList.Add("Интервал между прохождениями: " + test.Interval.Value.ToString(@"hh\:mm"));
                else
                    infoList.Add("Интервал между прохождениями отсутствует.");

                infoList.Add("Категория: " + test.Category.Value);
                infoList.Add("Автор: " + test.User.Name);
                infoList.Add("Дата создания: " + test.AddDate.ToShortDateString());
                infoList.Add("Система оценивания: " + test.RatingSystem.Value);

                if (test.User.Id == manager.User.Id)
                {
                    CtrlEditTest.Visibility = CtrlRemoveTest.Visibility = Visibility.Visible;

                    if (test.Attempts != null)
                        infoList.Add("Как автор вы имеете неограниченное количество попыток.");
                }

                if (!test.IsPrivate || manager.User.Id == test.User.Id || manager.Channel.GetInvitations().Any(i => i.Addressee.Id == test.User.Id && i.IsTransferable))
                    CtrlInvite.Visibility = Visibility.Visible;

                if(!test.Attempts.HasValue || test.UsedAttempts < test.Attempts)
                    CtrlTesting.Visibility = Visibility.Visible;

                foreach (var item in infoList)
                    CtrlStackInfo.Children.Add(new TextBlock()
                    {
                        Text = item,
                        Margin = new Thickness(4),
                        TextWrapping = TextWrapping.WrapWithOverflow
                    });
            }
        }
        #endregion

        AppManager manager;

        public TestInfoControl()
        {
            InitializeComponent();

            manager = AppManager.Instance;
            Clear();
        }
        #region Click

        private void CtrlTesting_Click(object sender, RoutedEventArgs e)
        {
            manager.CurControl = new TestingControl(testId.Value);
        }
        private void CtrlEditTest_Click(object sender, RoutedEventArgs e)
        {
            manager.TestEditControl.Use(testId);
        }
        private void CtrlInvite_Click(object sender, RoutedEventArgs e)
        {
            manager.CurMessageControl = new SendInvitationControl(testId.Value);
        }
        private void CtrlRemoveTest_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить тест?", "Удаление теста", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(!manager.Channel.RemoveTest(testId.Value))
                {
                    MessageBox.Show("Тест не найден.", "Ошибка");
                    return;
                }
                Clear();
                manager.MenuControl.Update();
            }
        }
        #endregion
        #region Update(), Clear()

        public void Update()
        {
            TestId = testId;
        }

        public void Clear()
        {
            testId = null;
            CtrlTesting.Visibility = CtrlEditTest.Visibility = CtrlRemoveTest.Visibility = CtrlInvite.Visibility = Visibility.Collapsed;
            CtrlStackInfo.Children.Clear();
            CtrlDescription.Text = string.Empty;
            CtrlTitle.Text = "Выберите тест для просмотра";
        }
        #endregion
    }
}
