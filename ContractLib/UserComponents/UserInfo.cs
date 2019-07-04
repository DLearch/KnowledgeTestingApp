using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.UserComponents
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailIsVisible { get; set; }
        public DateTime RegDate { get; set; }
        public string Password { get; set; }
        public int TestsCreated { get; set; }
        public int TestsCompleted { get; set; }
    }
}
