using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.UserComponents
{
    public class UserValidator
    {
        static uint 
            minNameLenght = 1, 
            maxNameLenght = 50, 
            minEmailLenght = 5, 
            maxEmailLenght = 50,
            minPasswordLenght = 6, 
            maxPasswordLenght = 50;

        public static string Error { get; private set; }

        public static bool IsValid(UserInfo user)
        {
            return IsValidName(user.Name)
                && IsValidEmail(user.Email)
                && IsValidPassword(user.Password);
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                if (minNameLenght == 0)
                    return true;

                Error = "Укажите имя.";
                return false;
            }

            if (name.Length > maxNameLenght)
            {
                Error = "Слишком длинное имя.";
                return false;
            }

            if (name.Length < minNameLenght)
            {
                Error = "Слишком короткое имя.";
                return false;
            }

            if (!char.IsLetter(name.First()))
            {
                Error = "Имя должно начинаться с буквы.";
                return false;
            }

            if (!name.All(c => char.IsDigit(c) || char.IsLetter(c)))
            {
                Error = "Имя должно содержать только цифры и буквы.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                if (minEmailLenght == 0)
                    return true;

                Error = "Укажите Email.";
                return false;
            }

            if (email.Length > maxEmailLenght)
            {
                Error = "Слишком длинный email.";
                return false;
            }

            if (email.Length < minEmailLenght)
            {
                Error = "Слишком короткий email.";
                return false;
            }

            try
            {
                new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                Error = "Неверный email.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                if (minPasswordLenght == 0)
                    return true;

                Error = "Укажите пароль.";
                return false;
            }

            if (password.Length > maxPasswordLenght)
            {
                Error = "Слишком длинный пароль.";
                return false;
            }

            if (password.Length < minPasswordLenght)
            {
                Error = "Слишком короткий пароль.";
                return false;
            }

            Error = string.Empty;
            return true;
        }
    }
}
