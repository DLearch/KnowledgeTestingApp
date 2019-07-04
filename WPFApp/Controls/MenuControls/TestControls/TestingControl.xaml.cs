using ContractLib.TestComponents;
using ContractLib.TestComponents.QuestionComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TestingControl.xaml
    /// </summary>
    public partial class TestingControl : UserControl
    {
        AppManager manager;
        TestInfo test;
        List<QuestionInfo> questions;
        TimeSpan duration;
        int curQuestionIndex;
        Dictionary<int, List<int>> results;
        Timer timer;
        GridLength lastLenght;
        public TestingControl(int testId)
        {
            InitializeComponent();

            manager = AppManager.Instance;

            test = manager.Channel.GetTest(testId);
            lastLenght = new GridLength(400);
            results = new Dictionary<int, List<int>>();

            if (test.Duration != null)
            {
                duration = new TimeSpan(0);
                timer = new Timer(new TimerCallback(UpdateTime), null, 0, 1000);
            }

            curQuestionIndex = -1;

            questions = manager.Channel.StartTest(testId);

            CtrlNext_Click(null, null);
            manager.WindowTitle = test.Title;
        }

        private void CtrlNext_Click(object sender, RoutedEventArgs e)
        {
            foreach (AnswerMinControl item in CtrlAnswers.Children)
            {
                if (item.Answer.IsCorrect == true)
                {
                    if (!results.ContainsKey(questions[curQuestionIndex].Id))
                        results.Add(questions[curQuestionIndex].Id, new List<int>());

                    results[questions[curQuestionIndex].Id].Add(item.Answer.Id);
                }
            }

            curQuestionIndex++;

            if (curQuestionIndex == questions.Count)
            {
                manager.Channel.FinishTest(results);
                manager.CurControl = new TestingResultControl(test.Id, duration);
                if (timer != null)
                    timer.Dispose();
                return;
            }
            if (curQuestionIndex == questions.Count - 1)
                CtrlNext.Content = "Завершить";
            else
                CtrlNext.Content = "Далее";

            CtrlAnswers.Children.Clear();
            foreach (AnswerInfo item in questions[curQuestionIndex].Answers)
                CtrlAnswers.Children.Add(new AnswerMinControl(item, questions[curQuestionIndex].IsRadio));

            CtrlQuestionText.Text = questions[curQuestionIndex].Text;
            if (questions[curQuestionIndex].Image != null && questions[curQuestionIndex].Image.Length > 0)
            {
                CtrlImage.Source = AppManager.GetBitmapImage(questions[curQuestionIndex].Image);
                CtrlColImage.MinWidth = 200;
                CtrlColImage.Width = lastLenght;
                CtrlColSplitterImage.Width = new GridLength(4);
            }
            else
            {
                lastLenght = CtrlColImage.Width;
                CtrlColImage.MinWidth = 0;
                CtrlColImage.Width = new GridLength(0);
                CtrlColSplitterImage.Width = new GridLength(0);
            }
        }

        private void CtrlBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null)
                timer.Dispose();
            manager.Channel.FinishTest(results);
            manager.MenuControl.Back();
        }

        public void UpdateTime(object obj)
        {
            Dispatcher.Invoke(()=>
            {
                duration = duration.Add(new TimeSpan(0, 0, 1));
                CtrlTime.Content = (test.Duration - duration).Value.ToString();
                if ((test.Duration - duration).Value.TotalMinutes < 3)
                    CtrlTime.Foreground = Brushes.Red;
                if((test.Duration - duration).Value.TotalSeconds == 0)
                {
                    manager.Channel.FinishTest(results);
                    manager.CurControl = new TestingResultControl(test.Id, duration, true);
                    timer.Dispose();
                }
            });
        }
    }
}
