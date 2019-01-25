using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryAccounting.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryAccounting.Models.DB
{
    public class CompaniesMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        //[Range(000000000, 999999999, ErrorMessage = "УНП должен состоять из 9 цифр")]
        [UnpUnique]
        [Display(Name = "УНП")]
        public int Unp { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [MaxLength(80)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(30)]
        [Display(Name = "Имя директора")]
        public string DirectorsName { get; set; }
        
        [MaxLength(15)]
        [Display(Name = "Телефон директора")]
        public string DirectorsPhone { get; set; }
    }
}
