using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public class PersonsMetaData
    {
        [Display(Name = "Табельный номер")]
        public int PersonnelNumber { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Паспортные данные")]
        public string PassportDetails { get; set; }
        [Display(Name = "Образование")]
        public string Education { get; set; }
        [Display(Name = "Дата трудоустройства")]
        public DateTime DateOfEmployment { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Display(Name = "Должность")]
        public string Post { get; set; }
    }
}
