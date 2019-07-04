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

namespace WPFApp.Controls.MenuControls.CatalogControls
{
    /// <summary>
    /// Interaction logic for TestCardControl.xaml
    /// </summary>
    public partial class TestCardControl : UserControl
    {
        public bool IsMin
        {
            get
            {
                return CtrlMinCard.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    CtrlMinCard.Visibility = Visibility.Visible;
                    CtrlMidCard.Visibility = Visibility.Collapsed;
                }
                else
                {
                    CtrlMinCard.Visibility = Visibility.Collapsed;
                    CtrlMidCard.Visibility = Visibility.Visible;
                }
            }
        }
        public bool IsSelected
        {
            get
            {
                return CtrlBorder.BorderBrush != CtrlBorder.Background;
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

        int testId;
        public int TestId
        {
            get
            {
                return testId;
            }
            set
            {
                testId = value;

                TestInfo test = AppManager.Instance.Channel.GetTest(testId);

                if (test.IsPrivate)
                    CtrlBorder.Background = (Brush)FindResource("PrivateColor");
                else
                    CtrlBorder.Background = (Brush)FindResource("PublicColor");

                MinCtrlTitle.Content = MidCtrlTitle.Text = test.Title;
                CtrlAuthor.Text = test.User.Name;
                CtrlDate.Text = test.AddDate.ToShortDateString();
                MidCtrlInfo.Children.Clear();
                
                List<string> infoList = new List<string>();

                if (!string.IsNullOrEmpty(test.Mark))
                    infoList.Add(test.Mark);

                MinCtrlAttempts.Text = test.UsedAttempts + "/";
                if (test.Attempts == null)
                    MinCtrlAttempts.Text += "∞";
                else
                    MinCtrlAttempts.Text += test.Attempts;

                infoList.Add(MinCtrlAttempts.Text);

                if (test.Duration != null)
                    infoList.Add("Время: " + test.Duration.Value.ToString(@"hh\:mm"));

                if (test.Interval != null)
                    infoList.Add("Интервал: " + test.Interval.Value.ToString(@"hh\:mm"));

                infoList.Add("Вопросы: " + test.QuestionsCount);

                foreach (var item in infoList)
                    MidCtrlInfo.Children.Add(new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = item
                    });
            }
        }

        public TestCardControl(int testId)
        {
            InitializeComponent();

            TestId = testId;
            IsMin = true;
            IsSelected = false;
        }

        public void Update()
        {
            TestId = testId;
        }
    }
}
