using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleServiceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        #region Свойства-Ссылки

        public virtual ICollection<Test> Tests { get; set; }
        #endregion
        #region Свойства

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        #endregion
    }
}
