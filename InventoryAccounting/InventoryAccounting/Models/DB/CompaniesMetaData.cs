using System;
using System.ComponentModel.DataAnnotations;
namespace InventoryAccounting.Models.DB
{
    public class CompaniesMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "УНП")]
        public int Unp { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [MaxLength(80)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Имя директора")]
        public string DirectorsName { get; set; }
        [MaxLength(15)]
        [Display(Name = "Телефон директора")]
        public string DirectorsPhone { get; set; }
    }
}
