using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContractLib.TestComponents.QuestionComponents
{
    public class AnswerInfo
    {
        public int Id { get; set; }
        
        public string Text { get; set; }

        public byte[] Image { get; set; }
        
        public bool IsCorrect { get; set; }
    }
}
