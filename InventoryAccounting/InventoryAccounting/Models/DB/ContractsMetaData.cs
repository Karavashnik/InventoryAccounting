using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryAccounting.Filters;

namespace InventoryAccounting.Models.DB
{
    public class ContractsMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [ForeignKey("Companies")]
        [Display(Name = "Компания")]
        public Guid CompanyId { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        //[ContractUnique]
        [Display(Name = "Номер договора")]
        public int ContractNumber { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [MaxLength(50)]
        [Display(Name = "Тип договора")]
        public string Type { get; set; }
        
        [Display(Name = "Срок договора")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? ExpirationDate { get; set; }
        
        [Required(ErrorMessage = "Поле {0} обязательное.")]
        [Display(Name = "Дата составления")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CompilationDate { get; set; }

        [Display(Name = "Компания")]
        public Companies Company { get; set; }
    }
}
