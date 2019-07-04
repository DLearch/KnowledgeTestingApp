using ContractLib.TestComponents.QuestionComponents;
using ContractLib.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents
{
    public class TestInfo
    {
        public int Id { get; set; }
        public UserInfo User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Attempts { get; set; }
        public TimeSpan? Interval { get; set; }
        public DateTime AddDate { get; set; }
        public KeyValuePair<int, string> Category { get; set; }
        public KeyValuePair<int, string> RatingSystem { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsQuestionsMix { get; set; }
        public string Mark { get; set; }
        public int UsedAttempts { get; set; }
        public int QuestionsCount { get; set; }
    }
}
