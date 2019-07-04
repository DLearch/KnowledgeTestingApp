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
using WPFApp.Controls.SignControls;

namespace WPFApp.Controls.MenuControls
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        AppManager manager;

        public ProfileControl()
        {
            InitializeComponent();
            manager = AppManager.Instance;

            CtrlName.Content = manager.User.Name;
            CtrlEmail.Content = manager.User.Email;
            CtrlTestsCompleted.Content = manager.User.TestsCompleted;
            CtrlTestsCreated.Content = manager.User.TestsCreated;

            CtrlEmailIsVisible.IsChecked = manager.User.EmailIsVisible;
        }

        #region Click

        private void CtrlChangePassword_Click(object sender, RoutedEventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;

            if (!CheckOldPassword() | !CheckNewPassword() | !CheckReNewPassword())
                return;

            if (manager.Channel.ChangeUserPassword(CtrlOldPassword.Password, CtrlNewPassword.Password))
            {
                manager.UpdateUser();
                CtrlChangePasswordMessage.Content = "Пароль успешно изменен.";
            }
            else
                CtrlErrorOldPassword.ShowError("Неправильный пароль");
        }
        private void CtrlSignOut_Click(object sender, RoutedEventArgs e)
        {
            manager.SignOut(CtrlClose.IsChecked == true);
        }
        private void CtrlEmailIsVisible_Click(object sender, RoutedEventArgs e)
        {
            manager.Channel.ChangeUserEmailVisiblity();
            manager.UpdateUser();
            CtrlEmail.Content = manager.User.Email;
        }
        #endregion
        #region LostFocus

        private void CtrlOldPassword_LostFocus(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CheckOldPassword();
        }

        private void CtrlNewPassword_LostFocus(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CheckNewPassword();
        }

        private void CtrlReNewPassword_LostFocus(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CheckReNewPassword();
        }
        #endregion
        #region KeyDown

        private void CtrlOldPassword_KeyDown(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CtrlErrorOldPassword.ClearError();
        }

        private void CtrlNewPassword_KeyDown(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CtrlErrorNewPassword.ClearError();
        }

        private void CtrlReNewPassword_KeyDown(object sender, EventArgs e)
        {
            CtrlChangePasswordMessage.Content = null;
            CtrlErrorReNewPassword.ClearError();
        }
        #endregion
        #region CheckOldPassword(), CheckNewPassword(), CheckReNewPassword()

        bool CheckOldPassword()
        {
            if (string.IsNullOrEmpty(CtrlOldPassword.Password))
            {
                CtrlErrorOldPassword.ShowError("Укажите старый пароль.");
                return false;
            }

            if (!UserValidator.IsValidPassword(CtrlOldPassword.Password))
            {
                CtrlErrorOldPassword.ShowError("Неправильный пароль.");
                return false;
            }

            CtrlErrorOldPassword.ClearError();
            return true;
        }

        bool CheckNewPassword()
        {
            if (string.IsNullOrEmpty(CtrlNewPassword.Password))
            {
                CtrlErrorNewPassword.ShowError("Укажите новый пароль.");
                return false;
            }

            if (!UserValidator.IsValidPassword(CtrlNewPassword.Password))
            {
                CtrlErrorNewPassword.ShowError(UserValidator.Error);
                return false;
            }

            CtrlErrorNewPassword.ClearError();
            return true;
        }

        bool CheckReNewPassword()
        {
            if (string.IsNullOrEmpty(CtrlReNewPassword.Password))
            {
                CtrlErrorReNewPassword.ShowError("Повторите новый пароль.");
                return false;
            }

            if (CtrlReNewPassword.Password != CtrlNewPassword.Password)
            {
                CtrlErrorReNewPassword.ShowError("Пароли не совпадают.");
                return false;
            }

            CtrlErrorReNewPassword.ClearError();
            return true;
        }

        #endregion
    }
}
