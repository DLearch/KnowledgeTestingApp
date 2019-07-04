using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleServiceApp.Models
{
    public class RatingSystem
    {
        public int Id { get; set; }
        #region Свойства-Ссылки

        public virtual ICollection<Test> Test { get; set; }
        #endregion
        #region Свойства

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
        #region Методы

        public string GetMark(int score, int maxScore)
        {
            double p = 100 * score / maxScore;

            switch (Name)
            {
                case "Процентная":
                    return ((int)p).ToString();
                case "ECTS":
                    if (p == 100)
                        return "A+";
                    if (p >= 95 && p <= 99)
                        return "A";
                    if (p >= 90 && p <= 94)
                        return "A-";
                    if (p >= 85 && p <= 89)
                        return "B+";
                    if (p >= 80 && p <= 84)
                        return "B";
                    if (p >= 75 && p <= 79)
                        return "B-";
                    if (p >= 70 && p <= 74)
                        return "C+";
                    if (p >= 65 && p <= 69)
                        return "C";
                    if (p >= 60 && p <= 64)
                        return "C-";
                    if (p >= 55 && p <= 59)
                        return "D+";
                    if (p >= 50 && p <= 54)
                        return "D";
                    if (p >= 0 && p <= 49)
                        return "F";
                    return null;
                case "Пятибалльная":
                    if (p == 100)
                        return "5+";
                    if (p >= 90 && p <= 99)
                        return "5";
                    if (p >= 86 && p <= 89)
                        return "5-";
                    if (p == 85)
                        return "4+";
                    if (p >= 71 && p <= 84)
                        return "4";
                    if (p >= 66 && p <= 80)
                        return "4-";
                    if (p == 65)
                        return "3+";
                    if (p >= 51 && p <= 64)
                        return "3";
                    if (p >= 46 && p <= 50)
                        return "3-";
                    if (p == 45)
                        return "2+";
                    if (p >= 31 && p <= 44)
                        return "2";
                    if (p >= 1 && p <= 30)
                        return "2-";
                    if (p == 0)
                        return "1";
                    return null;
                case "Двенадцатибалльная":
                    if (p == 100)
                        return "12";
                    if (p >= 90 && p <= 99)
                        return "11";
                    if (p >= 86 && p <= 89)
                        return "10";
                    if (p == 85)
                        return "9";
                    if (p >= 71 && p <= 84)
                        return "8";
                    if (p >= 66 && p <= 80)
                        return "7-";
                    if (p == 65)
                        return "6";
                    if (p >= 51 && p <= 64)
                        return "5";
                    if (p >= 46 && p <= 50)
                        return "5";
                    if (p == 45)
                        return "4";
                    if (p >= 31 && p <= 44)
                        return "2";
                    if (p >= 0 && p <= 30)
                        return "1";
                    return null;
                default:
                    return null;
            }
        }
        #endregion
    }
}
