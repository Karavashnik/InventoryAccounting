using System.ComponentModel.DataAnnotations;
namespace InventoryAccounting.Models.DB
{
    public class CompaniesMetaData
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
