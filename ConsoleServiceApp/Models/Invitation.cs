using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using ContractLib.TestComponents;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleServiceApp.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        #region Конструкторы

        public Invitation() { }
        public Invitation(InvitationInfo info)
        {
            Id = info.Id;
            IsTransferable = info.IsTransferable;
            Date = info.Date;
            if(info.Sender != null)
                SenderId = info.Sender.Id;
            if (info.Addressee != null)
                AddresseeId = info.Addressee.Id;
            TestId = info.TestId;
        }
        #endregion
        #region Свойства-Ссылки

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }

        [Required]
        [ForeignKey("Addressee")]
        public int AddresseeId { get; set; }
        public virtual User Addressee { get; set; }

        [Required]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
        #endregion
        #region Свойства

        [Required]
        public bool IsTransferable { get; set; }

        [Required]
        public DateTime Date { get; set; }
        #endregion
        #region Методы

        public InvitationInfo GetEncryptedInfo()
        {
            return new InvitationInfo()
            {
                Id = Id,
                Sender = Sender.GetEncryptedInfo(),
                Addressee = Addressee.GetEncryptedInfo(),
                Date = Date,
                IsTransferable = IsTransferable,
                TestId = Test.Id
            };
        }
        #endregion
    }
}
