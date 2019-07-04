using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContractLib.UserComponents;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleServiceApp.Models
{
    public class User
    {
        public int Id { get; set; }
        #region Конструкторы

        public User() { }
        public User(UserInfo info)
        {
            Id = info.Id;
            Name = info.Name;
            Email = info.Email;
            RegDate = info.RegDate;
            Password = info.Password;
            EmailIsVisible = info.EmailIsVisible;
        }
        #endregion
        #region Свойства-Ссылки

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Invitation> SentInvitations { get; set; }
        [InverseProperty("Addressee")]
        public ICollection<Invitation> ReceivedInvitations { get; set; }
        #endregion
        #region Свойства

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime RegDate { get; set; }

        [Required]
        public bool EmailIsVisible { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
        #endregion
        #region Методы

        public UserInfo GetEncryptedInfo()
        {
            UserInfo info = new UserInfo()
            {
                Id = Id,
                Name = Name,
                RegDate = RegDate,
                EmailIsVisible = EmailIsVisible,
                TestsCompleted = 0,
                TestsCreated = 0,
                Email = Email
            };

            if (TestResults != null)
                info.TestsCompleted = TestResults.Count();

            if (Tests != null)
                info.TestsCreated = Tests.Count();

            if (!EmailIsVisible)
            {
                string tmp = new string(Email.TakeWhile(c => c != '@').ToArray());
                int count = 2;
                if (tmp.Length < 4)
                    count = 1;
                if (tmp.Length == 1)
                    count = 0;
                tmp = new string(tmp.Take(count).ToArray()) + new string('*', tmp.Length - count);
                info.Email = tmp + new string(Email.SkipWhile(c => c != '@').ToArray());
            }
            info.Password = new string('*', 6);

            return info;
        }
        #endregion
    }
}
