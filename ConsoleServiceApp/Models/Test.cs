using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContractLib.TestComponents;
using System.Data.Entity;

namespace ConsoleServiceApp.Models
{
    public class Test
    {
        public int Id { get; set; }
        #region Конструкторы

        public Test() { }
        public Test(TestInfo info) : base()
        {
            Id = info.Id;
            if (info.User != null)
                UserId = info.User.Id;
            Title = info.Title;
            Description = info.Description;
            Duration = info.Duration;
            Attempts = info.Attempts;
            Interval = info.Interval;
            AddDate = info.AddDate;
            CategoryId = info.Category.Key;
            RatingSystemId = info.RatingSystem.Key;
            IsPrivate = info.IsPrivate;
            IsQuestionsMix = info.IsQuestionsMix;
        }
        #endregion
        #region Свойства-Ссылки

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public int RatingSystemId { get; set; }
        public virtual RatingSystem RatingSystem { get; set; }
        #endregion
        #region Свойства

        [Required]
        [MinLength(4)]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(8000)]
        public string Description { get; set; }

        public TimeSpan? Duration { get; set; }

        public int? Attempts { get; set; }

        public TimeSpan? Interval { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public bool IsQuestionsMix { get; set; }
        #endregion
        #region Методы

        public TestInfo GetInfo(int? userId = null)
        {
            TestInfo info = new TestInfo()
            {
                RatingSystem = new KeyValuePair<int, string>(RatingSystem.Id, RatingSystem.Name),
                Category = new KeyValuePair<int, string>(Category.Id, Category.Name),
                AddDate = AddDate,
                Attempts = Attempts,
                Description = Description,
                Duration = Duration,
                Interval = Interval,
                Id = Id,
                IsPrivate = IsPrivate,
                IsQuestionsMix = IsQuestionsMix,
                Title = Title,
                User = User.GetEncryptedInfo(),
                UsedAttempts = 0,
                Mark = string.Empty,
                QuestionsCount = 0
            };

            info.QuestionsCount = Questions.Count();

            if (userId.HasValue && TestResults != null)
            {
                TestResult testResult = TestResults.FirstOrDefault(tr => tr.UserId == userId.Value);

                if (testResult != null)
                {
                    info.UsedAttempts = testResult.Attempts;
                    info.Mark = RatingSystem.GetMark(testResult.Score, Questions.Sum(q => q.Weight));
                }
            }

            return info;
        }
        #endregion
    }
}
