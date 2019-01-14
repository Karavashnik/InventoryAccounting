using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public class ContractsMetaData
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        [Display(Name = "Компания")]
        public Guid CompanyId { get; set; }
        [Display(Name = "Номер договора")]
        public int ContractNumber { get; set; }
        [Display(Name = "Тип договора")]
        public string Type { get; set; }
        [Display(Name = "Скрок договора")]
        public DateTime? ExpirationDate { get; set; }
        [Display(Name = "Дата составления")]
        public DateTime CompilationDate { get; set; }

        [Display(Name = "Компания")]
        public Companies Company { get; set; }
    }
}
