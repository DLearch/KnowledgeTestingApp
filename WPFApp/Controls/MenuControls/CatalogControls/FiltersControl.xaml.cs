using ContractLib.TestComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for FiltersControl.xaml
    /// </summary>
    public partial class FiltersControl : UserControl
    {
        public event Action FiltersChanged;

        public TestFilter Filter
        {
            get
            {
                return new TestFilter()
                {
                    MinQuestionsCount = GetNum(CtrlMinQuestionsCount.Text),
                    MaxQuestionsCount = GetNum(CtrlMaxQuestionsCount.Text),
                    Categories = GetSelectedElements(CtrlCategoriesStack, CtrlCategoriesCheck.IsChecked),
                    RatingSystems = GetSelectedElements(CtrlRatingSystemsStack, CtrlRatingSystemsCheck.IsChecked)
                };
            }
        }

        AppManager manager;

        public FiltersControl()
        {
            InitializeComponent();

            manager = AppManager.Instance;
            UpdateCheckBoxStack(manager.Channel.GetCategories(), CtrlCategoriesStack, CtrlCategoriesCheck.IsChecked);
            UpdateCheckBoxStack(manager.Channel.GetRatingSystems(), CtrlRatingSystemsStack, CtrlRatingSystemsCheck.IsChecked);
        }

        #region Click

        private void CtrlApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void CtrlClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ClearCheckBoxStack(CtrlCategoriesStack, CtrlCategoriesCheck);
            ClearCheckBoxStack(CtrlRatingSystemsStack, CtrlRatingSystemsCheck);
            CtrlMaxQuestionsCount.Text = CtrlMinQuestionsCount.Text = string.Empty;
            FiltersChanged?.Invoke();
        }

        private void CtrlCategoriesCheck_Click(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxStack(manager.Channel.GetCategories(), CtrlCategoriesStack, CtrlCategoriesCheck.IsChecked);
        }

        private void CtrlRatingSystemsCheck_Click(object sender, RoutedEventArgs e)
        {
            UpdateCheckBoxStack(manager.Channel.GetRatingSystems(), CtrlRatingSystemsStack, CtrlRatingSystemsCheck.IsChecked);
        }
        #endregion
        #region UpdateCheckBoxStack(-), ClearCheckBoxStack(-), SwitchCheckBoxStack(-)

        void UpdateCheckBoxStack(Dictionary<int, string> dict, StackPanel stack, bool? state = true)
        {
            List<CheckBox> btns = new List<CheckBox>();

            foreach (var item in dict)
            {
                btns.Add(new CheckBox()
                {
                    Content = item.Value,
                    CommandParameter = item.Key
                });

                foreach (CheckBox checkBox in stack.Children)
                {
                    if ((int)(checkBox.CommandParameter) == item.Key)
                    {
                        btns.Last().IsChecked = checkBox.IsChecked;
                        break;
                    }
                }
            }

            stack.Children.Clear();
            foreach (var btn in btns)
                stack.Children.Add(btn);

            SwitchCheckBoxStack(stack, state);
        }
        void ClearCheckBoxStack(StackPanel stack, CheckBox checkBox)
        {
            foreach (CheckBox item in stack.Children)
                item.IsChecked = false;
            checkBox.IsChecked = null;
            checkBox.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        void SwitchCheckBoxStack(StackPanel stack, bool? state = true)
        {
            foreach (CheckBox item in stack.Children)
            {
                item.IsEnabled = state != null;
                item.Style = (Style)FindResource(state == true ? "OnlyTrueCheckBox" : "OnlyFalseCheckBox");
            }
        }
        #endregion
        #region GetNum(-), GetSelectedElements(-)

        int? GetNum(string s)
        {
            int num;

            if (int.TryParse(s, out num))
                return num;

            return null;
        }
        List<int> GetSelectedElements(StackPanel stack, bool? state)
        {
            if (state == null)
                return null;

            List<int> elements = new List<int>();

            foreach (CheckBox item in stack.Children)
                if (item.IsChecked == state && state == true || item.IsChecked == state && state == false)
                    elements.Add((int)(item.CommandParameter));

            return elements;
        }
        #endregion
        #region PreviewTextInput

        private void CtrlQuestionsCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
        #endregion
    }
}
