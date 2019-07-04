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

namespace WPFApp.Controls.MenuControls.TestControls
{
    /// <summary>
    /// Interaction logic for TestingResultControl.xaml
    /// </summary>
    public partial class TestingResultControl : UserControl
    {
        AppManager manager;
        int testId;

        public TestingResultControl(int testId, TimeSpan? time, bool isTimeEnd = false)
        {
            InitializeComponent();
            manager = AppManager.Instance;
            TestInfo test = manager.Channel.GetTest(testId);

            CtrlTitle.Text = test.Title;

            if (isTimeEnd)
                CtrlTime.Text = "Время закончилось";
            else if (time.HasValue)
                CtrlTime.Text = time.Value.ToString();

            CtrlMark.Text = test.Mark;

            CtrlAttempts.Content = test.UsedAttempts + "/";

            if (test.Attempts.HasValue)
                CtrlAttempts.Content += test.Attempts.Value.ToString();
            else
                CtrlAttempts.Content += "∞";

            if (!test.Attempts.HasValue || test.UsedAttempts < test.Attempts.Value)
                CtrlReTesting.Visibility = Visibility.Visible;
            else
                CtrlReTesting.Visibility = Visibility.Collapsed;
            
            this.testId = testId;
        }

        private void CtrlReTesting_Click(object sender, RoutedEventArgs e)
        {
            manager.CurControl = new TestingControl(testId);
        }

        private void CtrlBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            manager.MenuControl.Back();
        }
    }
}
