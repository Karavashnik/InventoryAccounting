using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAccounting.Models.DB
{
    public class RoomsMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(10)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Этаж")]
        public int Floor { get; set; }
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Номер")]
        public int Number { get; set; }
        [MaxLength(15)]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }
}
