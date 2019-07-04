using ContractLib.TestComponents;
using ContractLib.UserComponents;
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

namespace WPFApp.Controls.MenuControls.InvitationsControls
{
    /// <summary>
    /// Interaction logic for SendInvitationControl.xaml
    /// </summary>
    public partial class SendInvitationControl : UserControl
    {
        AppManager manager;
        int testId;

        public SendInvitationControl(int testId)
        {
            InitializeComponent();

            manager = AppManager.Instance;

            this.testId = testId;
        }
        #region Click 

        private void ButtonInvite_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckName())
                return;

            UserInfo user = manager.Channel.GetUserByName(CtrlName.Text);

            if (user != null)
            {
                manager.Channel.SendInvitation(new InvitationInfo()
                {
                    TestId = testId,
                    Addressee = user,
                    IsTransferable = CtrlIsTransferableCheck.IsChecked == true
                });
                manager.CurMessageControl = null;
            }
            else
                CtrlErrorName.ShowError("Пользователь не найден.");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            manager.CurMessageControl = null;
        }
        #endregion
        #region LostFocus, KeyDown, KeyUp

        private void ButtonInvite_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckName();
        }

        private void ButtonInvite_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorName.ClearError();
        }
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                manager.CurMessageControl = null;
            if (e.Key == Key.Enter)
                ButtonInvite_Click(null, null);
        }
        #endregion
        #region Check

        bool CheckName()
        {
            CtrlErrorName.ClearError();

            if (!UserValidator.IsValidName(CtrlName.Text))
            {
                CtrlErrorName.ShowError(UserValidator.Error);
                return false;
            }

            if (manager.Channel.GetUserByName(CtrlName.Text) == null)
            {
                CtrlErrorName.ShowError("Пользователь не найден.");
                return false;
            }

            return true;
        }
        #endregion
        
    }
}
