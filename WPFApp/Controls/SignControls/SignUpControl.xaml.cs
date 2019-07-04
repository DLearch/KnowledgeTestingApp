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
using WPFApp.Controls.MenuControls;

namespace WPFApp.Controls.SignControls
{
    /// <summary>
    /// Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl
    {
        AppManager manager;

        public SignUpControl()
        {
            InitializeComponent();

            manager = AppManager.Instance;
            manager.WindowTitle = "Регистрация";
        }

        #region Click

        private void CtrlNext_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckAllFields())
                return;

            manager.Channel.Register(new UserInfo() { Name = CtrlName.Text, Email = CtrlEmail.Text, Password = CtrlPassword.Password });
            manager.SignIn();
        }
        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            manager.CurControl = new SignInControl();
        }
        #endregion
        #region KeyDown

        private void CtrlName_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorName.ClearError();
            CtrlError.ClearError();
        }
        private void CtrlEmail_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorEmail.ClearError();
            CtrlError.ClearError();
        }
        private void CtrlPassword_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorPassword.ClearError();
            CtrlError.ClearError();
        }
        private void CtrlRePassword_KeyDown(object sender, KeyEventArgs e)
        {
            CtrlErrorRePassword.ClearError();
            CtrlError.ClearError();
        }
        #endregion
        #region LostFocus

        private void CtrlName_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckName();
        }
        private void CtrlEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckEmail();
        }
        private void CtrlPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckPassword();
        }
        private void CtrlRePassword_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckRePassword();
        }
        #endregion
        #region CheckAllFields(), CheckName(), CheckEmail(), CheckPassword(), CheckRePassword()

        bool CheckAllFields()
        {
            return CheckName() & CheckEmail() & CheckPassword() & CheckRePassword();
        }

        bool CheckName()
        {
            if (!UserValidator.IsValidName(CtrlName.Text))
            {
                CtrlErrorName.ShowError(UserValidator.Error);
                return false;
            }
            if (!manager.Channel.UserNameIsAvailable(CtrlName.Text))
            {
                CtrlErrorName.ShowError("Это имя занято.");
                return false;
            }

            CtrlErrorName.ClearError();
            CtrlError.ClearError();
            return true;
        }
        bool CheckEmail()
        {
            if (!UserValidator.IsValidEmail(CtrlEmail.Text))
            {
                CtrlErrorEmail.ShowError(UserValidator.Error);
                return false;
            }
            if (!manager.Channel.UserEmailIsAvailable(CtrlEmail.Text))
            {
                CtrlErrorEmail.ShowError("Этот email занят.");
                return false;
            }
            CtrlErrorEmail.ClearError();
            CtrlError.ClearError();

            return true;
        }
        bool CheckPassword()
        {
            if (!UserValidator.IsValidPassword(CtrlPassword.Password))
            {
                CtrlErrorPassword.ShowError(UserValidator.Error);
                return false;
            }
            CtrlErrorPassword.ClearError();
            CtrlError.ClearError();

            return true;
        }
        bool CheckRePassword()
        {
            if (CtrlRePassword.Password != CtrlPassword.Password)
            {
                CtrlErrorRePassword.ShowError("Пароли не совпадают.");
                return false;
            }
            CtrlErrorRePassword.ClearError();
            CtrlError.ClearError();

            return true;
        }
        #endregion
    }
}
