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

namespace WPFApp.Controls.AdditionalControls
{
    /// <summary>
    /// Interaction logic for PageNavigatorControl.xaml
    /// </summary>
    public partial class PageNavigatorControl : UserControl
    {
        public event Action IndexChanged;

        int curPageNum;
        public int CurPageNumber
        {
            get
            {
                return curPageNum;
            }
            set
            {
                curPageNum = value;
                CtrlStack.Children.Clear();

                int blockCount = 2;

                for (int i = 1; i <= blockCount; i++)
                {
                    if (i >= value - blockCount || i > pagesCount)
                        continue;

                    CtrlStack.Children.Add(GetPageButton(i));
                }

                if (value - blockCount > 3)
                    CtrlStack.Children.Add(new Label() { Content = "...", FontSize = 16, VerticalAlignment = VerticalAlignment.Bottom });

                for (int i = value - blockCount; i <= value + blockCount; i++)
                {
                    if (i < 1 || i > pagesCount)
                        continue;

                    CtrlStack.Children.Add(GetPageButton(i, i != value));
                }

                if (value + blockCount < pagesCount - blockCount)
                    CtrlStack.Children.Add(new Label() { Content = "...", FontSize = 16, VerticalAlignment = VerticalAlignment.Bottom });


                for (int i = pagesCount - 1; i <= pagesCount; i++)
                {
                    if (i <= value + blockCount)
                        continue;

                    CtrlStack.Children.Add(GetPageButton(i));
                }

                CtrlNextBtn.IsEnabled = value < pagesCount;
                CtrlPreviousBtn.IsEnabled = value > 1;

                IndexChanged?.Invoke();
            }
        }

        int elementsCount;
        public int ElementsCount
        {
            get
            {
                return elementsCount;
            }
            set
            {
                elementsCount = value;

                pagesCount = ElementsCount / elementsOnPageCount;

                if ((double)ElementsCount / elementsOnPageCount > pagesCount || pagesCount == 0)
                    pagesCount++;

                if (CurPageNumber > pagesCount)
                    CurPageNumber = pagesCount - 1;
                else
                    CurPageNumber = CurPageNumber;
            }
        }

        int elementsOnPageCount;
        public int ElementsOnPageCount
        {
            get
            {
                return elementsOnPageCount;
            }
        }

        public int PageFirstElementIndex
        {
            get
            {
                return (CurPageNumber - 1) * ElementsOnPageCount;
            }
        }

        public int PageLastElementIndex
        {
            get
            {
                int tmp = PageFirstElementIndex + ElementsOnPageCount;
                if (tmp < ElementsCount)
                    return tmp;

                return ElementsCount;
            }
        }
        int pagesCount;

        public PageNavigatorControl()
        {
            InitializeComponent();
            elementsCount = 1;
            CurPageNumber = 1;
            CtrlElementsCount.SelectedIndex = 1;
        }

        #region Click

        private void ButtonPage_Click(object sender, RoutedEventArgs e)
        {
            CurPageNumber = int.Parse((sender as Button).Content.ToString());
        }
        private void CtrlPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            CurPageNumber--;
        }
        private void CtrlNextBtn_Click(object sender, RoutedEventArgs e)
        {
            CurPageNumber++;
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            ElementsCount = ElementsCount;
        }
        #endregion
        #region GetPageButton(-)

        Button GetPageButton(int num, bool isEnabled = true)
        {
            Button button;

            button = new Button()
            {
                Content = num,
                Style = (Style)FindResource("PageIndexButton")
            };

            button.Click += ButtonPage_Click;
            button.IsEnabled = isEnabled;

            return button;
        }

        #endregion
        #region SelectionChanged

        private void ComboBoxElementsCount_SelectionChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            elementsOnPageCount = int.Parse(selectedItem.Content.ToString());
            
            ElementsCount = ElementsCount;
        }
        #endregion
    }
}
