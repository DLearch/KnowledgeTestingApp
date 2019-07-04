using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContractLib.TestComponents.QuestionComponents;
using System.Data.Entity;

namespace ConsoleServiceApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        #region Конструкторы

        public Question() { }
        public Question(QuestionInfo info)
        {
            Id = info.Id;
            Image = info.Image;
            Text = info.Text;
            IsRadio = info.IsRadio;
            Weight = info.Weight;
            IsAnswersMix = info.IsAnswersMix;
        }
        #endregion
        #region Свойства-Ссылки

        [Required]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        #endregion
        #region Свойства

        [Required]
        [MaxLength(8000)]
        public string Text { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public bool IsRadio { get; set; }

        [Required]
        public bool IsAnswersMix { get; set; }

        [Required]
        public int Weight { get; set; }
        #endregion
        #region Методы

        public QuestionInfo GetInfo()
        {
            return new QuestionInfo()
            {
                Id = Id,
                Image = Image,
                IsAnswersMix = IsAnswersMix,
                IsRadio = IsRadio,
                Text = Text,
                Weight = Weight,
                Answers = Answers.Select(a => a.GetEncryptedInfo()).ToList()
            };
        }

        public QuestionInfo GetEncryptedInfo()
        {
            QuestionInfo info = GetInfo();

            info.Answers = Answers.Select(a => a.GetEncryptedInfo()).ToList();
            return info;
        }
        #endregion
    }
}
