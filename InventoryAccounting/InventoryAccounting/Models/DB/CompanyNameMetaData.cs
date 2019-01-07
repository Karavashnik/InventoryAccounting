using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAccounting.Models.DB
{
    public class CompanyNameMetaData
    {
        [Display(Name = "УНП")]
        public int Unp { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Имя директора")]
        public string DirectorsName { get; set; }
        [Display(Name = "Телефон директора")]
        public string DirectorsPhone { get; set; }
    }
}
