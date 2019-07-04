using ContractLib.TestComponents.QuestionComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents
{
    public class TestValidator
    {
        static uint 
            minTitleLenght = 4, 
            maxTitleLenght = 150,
            minDescriptionLenght = 0,  
            maxDescriptionLenght = 8000, 
            minQuestionsCount = 1,
            maxQuestionsCount = 10000;

        public static string Error { get; set; }

        public static bool IsValid(TestInfo test)
        {
            return IsValidTitle(test.Title)
                && IsValidDescription(test.Description);
        }

        public static bool IsValidTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                if (minTitleLenght == 0)
                    return true;

                Error = "Укажите название.";
                return false;
            }

            if (title.Length > maxTitleLenght)
            {
                Error = "Слишком длинное название.";
                return false;
            }
            if (title.Length < minTitleLenght)
            {
                Error = "Слишком короткое название.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                if (minDescriptionLenght == 0)
                    return true;

                Error = "Укажите описание.";
                return false;
            }

            if (description.Length > maxDescriptionLenght)
            {
                Error = "Слишком длинное описание.";
                return false;
            }

            if (description.Length < minDescriptionLenght)
            {
                Error = "Слишком короткое описание.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidQuestions(List<QuestionInfo> questions)
        {
            if (questions == null || questions.Count == 0)
            {
                if (minQuestionsCount == 0)
                    return true;

                Error = "Отсутствуют вопросы.";
                return false;
            }

            if (questions.Count > maxQuestionsCount)
            {
                Error = "Слишком много вопросов.";
                return false;
            }

            if (questions.Count < minQuestionsCount)
            {
                Error = "Слишком мало вопросов.";
                return false;
            }

            if (questions.Any(q => !QuestionValidator.IsValid(q)))
            {
                Error = "Присутствуют невалидные вопросы.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
    }
}
