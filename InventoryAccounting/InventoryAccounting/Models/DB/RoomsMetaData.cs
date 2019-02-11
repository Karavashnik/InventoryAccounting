using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public class RoomsMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(15)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Range(6,13, ErrorMessage = "Введите значение в диапазоне от {1} до {2}")]
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Этаж")]
        public int? Floor { get; set; }
        
        [Range(1,15, ErrorMessage = "Введите значение в диапазоне от {1} до {2}")]
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Номер")]
        public int? Number { get; set; }
        
        [MaxLength(5)]
        [RegularExpression(@"[0-2][0-9]-[0-2][0-9]", ErrorMessage = "Телефон должен быть в формате XX-XX"), StringLength(5)]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }
}
