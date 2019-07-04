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

namespace WPFApp.Controls.MenuControls.InvitationsControls
{
    /// <summary>
    /// Interaction logic for InvitationCardControl.xaml
    /// </summary>
    public partial class InvitationCardControl : UserControl
    {
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

        public InvitationInfo Invitation
        {
            get
            {
                return invitation;
            }
            set
            {
                CtrlBorder.Background = (Brush)FindResource("PublicColor");
                invitation = value;

                if(value == null)
                {
                    CtrlDate.Content = 
                        CtrlSender.Content = 
                        CtrlTitle.Content = string.Empty;

                    return;
                }
                TestInfo test = AppManager.Instance.Channel.GetTest(value.TestId);

                if (test.IsPrivate)
                    CtrlBorder.Background = (Brush)FindResource("PrivateColor");


                CtrlDate.Content = value.Date.ToString();
                CtrlSender.Content = value.Sender.Name;
                CtrlTitle.Content = test.Title;
            }
        }

        InvitationInfo invitation;

        public InvitationCardControl(InvitationInfo invitation)
        {
            InitializeComponent();
            IsSelected = false;
            Invitation = invitation;
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            AppManager.Instance.Channel.RemoveInvitation(invitation.Id);
            AppManager.Instance.MenuControl.Update();
        }
    }
}
