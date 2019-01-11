using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAccounting.Models.DB
{
    public class ActsMetaData
    {
        public int Id { get; set; }
        [Display(Name = "Номер акта")]
        public int ActNumber { get; set; }
        [Display(Name = "Договор")]
        public Guid? ContractId { get; set; }
        [Display(Name = "Дата составления")]
        public DateTime CompilationDate { get; set; }

        [Display(Name = "Договор")]
        public Contracts Contract { get; set; }
    }
}
