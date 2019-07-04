using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleServiceApp.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        #region Свойства-Ссылки

        [Required]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
        #region Свойства

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public int Attempts { get; set; }
        #endregion
    }
}
