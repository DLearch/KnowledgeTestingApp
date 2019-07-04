using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents.QuestionComponents
{
    public class AnswerValidator
    {
        static uint 
            minTextLenght = 0,
            maxTextLenght = 8000;

        public static string Error { get; set; }

        public static bool IsValid(AnswerInfo answer)
        {
            return IsValidText(answer.Text);
        }

        public static bool IsValidText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (minTextLenght == 0)
                    return true;

                Error = "Укажите ответ.";
                return false;
            }

            if (text.Length > maxTextLenght)
            {
                Error = "Слишком длинный ответ.";
                return false;
            }

            if (text.Length < minTextLenght)
            {
                Error = "Слишком короткий ответ.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
    }
}
