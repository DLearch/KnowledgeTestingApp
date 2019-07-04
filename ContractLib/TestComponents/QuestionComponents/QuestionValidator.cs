using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents.QuestionComponents
{
    public class QuestionValidator
    {
        static uint
            minTextLenght = 0,
            maxTextLenght = 8000,
            minAnswersCount = 1,
            maxAnswersCount = 100;

        static int
            minWeight = -1000,
            maxWeight = 1000;

        public static string Error { get; set; }

        public static bool IsValid(QuestionInfo question)
        {
            return IsValidText(question.Text)
                && IsValidWeight(question.Weight)
                && IsValidAnswers(question.Answers, question.IsRadio);
        }

        public static bool IsValidText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (minTextLenght == 0)
                    return true;

                Error = "Укажите вопрос.";
                return false;
            }

            if (text.Length > maxTextLenght)
            {
                Error = "Слишком длинный вопрос.";
                return false;
            }

            if (text.Length < minTextLenght)
            {
                Error = "Слишком короткий вопрос.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidWeight(int weight)
        {
            if (weight < minWeight)
            {
                Error = "Слишком маленький вес.";
                return false;
            }

            if (weight > maxWeight)
            {
                Error = "Слишком большой вес.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidAnswers(List<AnswerInfo> answers, bool? isRadio = null)
        {
            if (answers == null || answers.Count == 0)
            {
                if(minAnswersCount == 0)
                    return true;

                Error = "Отсутствуют ответы.";
                return false;
            }

            if (answers.Count > maxAnswersCount)
            {
                Error = "Слишком много ответов.";
                return false;
            }

            if (answers.Count < minAnswersCount)
            {
                Error = "Слишком мало ответов.";
                return false;
            }

            if (answers.Any(a => !AnswerValidator.IsValid(a)))
            {
                Error = "Присутствуют невалидные ответы.";
                return false;
            }

            if (isRadio != null && isRadio == true && answers.All(a => a.IsCorrect != true))
            {
                Error = "Отсутствует правильный ответ.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
    }
}
