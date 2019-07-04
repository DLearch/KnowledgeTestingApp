using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents.QuestionComponents
{
    public class QuestionInfo
    {
        public int Id { get; set; }
        
        public string Text { get; set; }

        public byte[] Image { get; set; }
        
        public bool IsRadio { get; set; }
        
        public bool IsAnswersMix { get; set; }
        
        public int Weight { get; set; }

        public List<AnswerInfo> Answers { get; set; }
    }
}
