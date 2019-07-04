using ContractLib.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents
{
    public class InvitationInfo
    {
        public int Id { get; set; }

        public UserInfo Sender { get; set; }

        public UserInfo Addressee { get; set; }

        public bool IsTransferable { get; set; }

        public DateTime Date { get; set; }

        public int TestId { get; set; }
    }
}
