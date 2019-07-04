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
    /// Interaction logic for CatalogControl.xaml
    /// </summary>
    public partial class CatalogControl : UserControl
    {
        int? userId;
        AppManager manager;
        TestCardControl selectedCard;
        List<int> testIdList;

        public CatalogControl(int? userId = null)
        {
            InitializeComponent();

            manager = AppManager.Instance;
            
            this.userId = userId;

            CtrlFilter.FiltersChanged += UpdatePagesNav;
            CtrlPageNav.IndexChanged += UpdatePage;
            UpdatePagesNav();
        }

        #region Update

        public void UpdatePagesNav()
        {
            TestFilter filter = CtrlFilter.Filter;
            filter.Title = CtrlFindTestTitle.Text;

            testIdList = manager.Channel.GetTestsId(filter, userId);

            CtrlPageNav.ElementsCount = testIdList.Count;
        }
        void UpdatePage()
        {
            CtrlTestsWrap.Children.Clear();

            TestCardControl card;
            for (int i = CtrlPageNav.PageFirstElementIndex; i < CtrlPageNav.PageLastElementIndex; i++)
            {
                card = new TestCardControl(testIdList[i]);
                card.MouseLeftButtonUp += CtrlTestCard_MouseLeftButtonUp;
                card.IsMin = CtrlCardState.IsChecked == false;
                CtrlTestsWrap.Children.Add(card);
            }

            if (CtrlTestsWrap.Children.Count == 0)
                CtrlTestsEmpty.Visibility = Visibility.Visible;
            else
                CtrlTestsEmpty.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region Click

        private void CtrlAddTest_Click(object sender, RoutedEventArgs e)
        {
            manager.TestEditControl.Use();
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdatePagesNav();
        }
        private void CtrlCardState_Click(object sender, RoutedEventArgs e)
        {
            foreach (TestCardControl item in CtrlTestsWrap.Children)
            {
                item.IsMin = !item.IsMin;
            }
        }

        private void CtrlShowFilters_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Content.ToString() == "Показать фильтры")
            {
                CtrlColFilter.MinWidth = 250;
                CtrlColFilter.Width = new GridLength(250);
                CtrlColFilterSplitter.Width = new GridLength(4);
                btn.Content = "Спрятать фильтры";
            }
            else
            {
                CtrlColFilter.MinWidth = 0;
                CtrlColFilter.Width = new GridLength(0);
                CtrlColFilterSplitter.Width = new GridLength(0);
                btn.Content = "Показать фильтры";
            }
        }
        #endregion
        #region KeyUp, MouseLeftButtonUp, SelectionChanged

        private void CtrlFindTestTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UpdatePagesNav();
        }
        private void CtrlTestCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedCard != null)
                selectedCard.IsSelected = false;
            selectedCard = sender as TestCardControl;
            CtrlTestInfo.TestId = selectedCard.TestId;
            selectedCard.IsSelected = true;
            selectedCard.Focus();
        }
        #endregion
    }
}
