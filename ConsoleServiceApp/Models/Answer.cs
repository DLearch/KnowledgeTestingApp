using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContractLib.TestComponents.QuestionComponents;

namespace ConsoleServiceApp.Models
{
    public class Answer
    {
        public int Id { get; set; }
        #region Конструкторы

        public Answer() { }
        public Answer(AnswerInfo info)
        {
            Id = info.Id;
            Text = info.Text;
            Image = info.Image;
            IsCorrect = info.IsCorrect;
        }
        #endregion
        #region Свойства-Ссылки

        [Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        #endregion
        #region Свойства
        
        [Required]
        [MaxLength(8000)]
        public string Text { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
        #endregion
        #region Методы

        public AnswerInfo GetInfo()
        {
            return new AnswerInfo()
            {
                Id = Id,
                Text = Text,
                Image = Image,
                IsCorrect = IsCorrect
            };
        }

        public AnswerInfo GetEncryptedInfo()
        {
            AnswerInfo info = GetInfo();

            info.IsCorrect = false;

            return info;
        }
        #endregion
    }
}
