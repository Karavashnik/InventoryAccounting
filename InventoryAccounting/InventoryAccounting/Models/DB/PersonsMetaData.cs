using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace InventoryAccounting.Models.DB
{
    public class PersonsMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Табельный номер")]
        public int PersonnelNumber { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Паспортные данные")]
        public string PassportDetails { get; set; }
        [MaxLength(50)]
        [Display(Name = "Образование")]
        public string Education { get; set; }
        [Required]
        [Display(Name = "Дата трудоустройства")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfEmployment { get; set; }
        [MaxLength(15)]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [MaxLength(50)]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Должность")]
        public string Post { get; set; }
    }
}
