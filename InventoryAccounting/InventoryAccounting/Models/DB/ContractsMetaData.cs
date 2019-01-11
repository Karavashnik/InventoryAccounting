using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public class ContractsMetaData
    {
        [Display(Name = "Номер договора")]
        public int ContractNumber { get; set; }
        [Display(Name = "Компания")]
        public int CompanyUnp { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Компания")]
        public Companies Company { get; set; }
    }
}
