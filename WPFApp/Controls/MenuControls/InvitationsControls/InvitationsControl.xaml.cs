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
    /// Interaction logic for InvitationsControl.xaml
    /// </summary>
    public partial class InvitationsControl : UserControl
    {
        AppManager manager;
        List<InvitationInfo> invitations;
        InvitationCardControl selectedCard;

        public InvitationsControl()
        {
            InitializeComponent();

            manager = AppManager.Instance;

            CtrlPageNav.IndexChanged += UpdatePage;
            UpdatePagesNav();
        }

        public void UpdatePagesNav()
        {
            invitations = manager.Channel.GetInvitations();

            CtrlPageNav.ElementsCount = invitations.Count;
        }
        void UpdatePage()
        {
            CtrlInvitationsWrap.Children.Clear();

            InvitationCardControl card;
            
            for (int i = CtrlPageNav.PageFirstElementIndex; i < CtrlPageNav.PageLastElementIndex; i++)
            {
                card = new InvitationCardControl(invitations[i]);
                card.MouseLeftButtonUp += CtrlInvitationCard_MouseLeftButtonUp;
                CtrlInvitationsWrap.Children.Add(card);
            }

            if (CtrlInvitationsWrap.Children.Count == 0)
                CtrlInvitationsEmpty.Visibility = Visibility.Visible;
            else
                CtrlInvitationsEmpty.Visibility = Visibility.Collapsed;
        }
        private void CtrlInvitationCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedCard != null)
                selectedCard.IsSelected = false;
            selectedCard = sender as InvitationCardControl;
            CtrlTestInfo.TestId = selectedCard.Invitation.TestId;
            selectedCard.IsSelected = true;
            selectedCard.Focus();
        }
    }
}
