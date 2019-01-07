using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public CompanyName CompanyUnpNavigation { get; set; }
    }
}
