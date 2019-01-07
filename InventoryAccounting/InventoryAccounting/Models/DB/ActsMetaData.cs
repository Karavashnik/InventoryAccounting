using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAccounting.Models.DB
{
    public class ActsMetaData
    {
        [Display(Name = "Номер акта")]
        public int Id { get; set; }
        [Display(Name = "Дата составления")]
        public DateTime CompilationDate { get; set; }
        [Display(Name = "Договор")]
        public int ContractNumber { get; set; }

        [Display(Name = "Договор")]
        public Contracts ContractNumberNavigation { get; set; }
    }
}
