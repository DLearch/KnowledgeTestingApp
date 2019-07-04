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
    /// Interaction logic for SignInControl.xaml
    /// </summary>
    public partial class SignInControl : UserControl
    {
        AppManager manager;

        public SignInControl()
        {
            InitializeComponent();

            manager = AppManager.Instance;
            manager.WindowTitle = "Вход";

            CtrlError.Controls = new List<Control>() { CtrlName, CtrlPassword };
        }

        #region Click

        private void CtrlSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckPassword() | !CheckName())
                return;

            if (UserValidator.IsValidName(CtrlName.Text)
                && UserValidator.IsValidPassword(CtrlPassword.Password)
                && manager.Channel.SignIn(CtrlName.Text, CtrlPassword.Password))
            {
                manager.SignIn();
            }
            else
                CtrlError.ShowError("Неправильные данные.");
        }

        private void CtrlRegistration_Click(object sender, RoutedEventArgs e)
        {
            manager.CurControl = new SignUpControl();
        }
        #endregion
        #region KeyDown
        private void CtrlName_KeyDown(object sender, EventArgs e)
        {
            CtrlError.ClearError();
            CtrlErrorName.ClearError();
        }

        private void CtrlPassword_KeyDown(object sender, EventArgs e)
        {
            CtrlError.ClearError();
            CtrlErrorPassword.ClearError();
        }
        #endregion
        #region LostFocus
        private void CtrlName_LostFocus(object sender, EventArgs e)
        {
            CheckName();
        }

        private void CtrlPassword_LostFocus(object sender, EventArgs e)
        {
            CheckPassword();
        }
        #endregion
        #region CheckName(), CheckPassword()

        bool CheckName()
        {
            if (string.IsNullOrEmpty(CtrlName.Text))
            {
                CtrlErrorName.ShowError("Укажите имя.");
                return false;
            }

            CtrlErrorName.ClearError();
            CtrlError.ClearError();

            return true;
        }

        bool CheckPassword()
        {
            if (string.IsNullOrEmpty(CtrlPassword.Password))
            {
                CtrlErrorPassword.ShowError("Укажите пароль.");
                return false;
            }

            CtrlErrorPassword.ClearError();
            CtrlError.ClearError();

            return true;
        }

        #endregion
    }
}
